using Asp.Versioning;
using HIMS.Api.Models.Login;
using HIMS.API.Security;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.API.Controllers.Login
{
    [Route("api/v{version:apiVersion}/auth")]
    [ApiController]
    [ApiVersion("1")]
    [AllowAnonymous]
    public sealed class PatientOtpAuthController : ControllerBase
    {
        private readonly HIMSDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly ILoginV2KeyProvider _loginKeyProvider;

        public PatientOtpAuthController(HIMSDbContext db, IConfiguration configuration, ILoginV2KeyProvider loginKeyProvider)
        {
            _db = db;
            _configuration = configuration;
            _loginKeyProvider = loginKeyProvider;
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendPatientOtpRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new PatientOtpStatusResponse { Status = "error", Message = "Invalid request." });

            long mobile;
            if (!long.TryParse(request.MobileNumber, out mobile))
                return BadRequest(new PatientOtpStatusResponse { Status = "error", Message = "Invalid mobile number." });

            var patient = await _db.SsMobileAppLogins
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerMobileNo == request.MobileNumber);

            if (patient == null)
                return NotFound(new PatientOtpStatusResponse
                {
                    Status = "error",
                    Message = "Patient not found. Please sign up."
                });

            var delivery = await ResolveOtpDeliveryAsync();
            string configuredDefaultOtp = _configuration["PatientOtp:DefaultOtp"] ?? "123456";
            string otp = (!delivery.SmsEnabled && !delivery.WhatsAppEnabled)
                ? (configuredDefaultOtp.Length >= 4 ? configuredDefaultOtp : "123456")
                : RandomNumberGenerator.GetInt32(100000, 1000000).ToString("D6");
            int otpExpiryMinutes = _configuration.GetValue("PatientOtp:OtpExpiryMinutes", 5);
            if (otpExpiryMinutes < 1) otpExpiryMinutes = 5;

            var record = new TProcessOtp
            {
                MobileNo = mobile,
                Otpno = otp,
                Message = $"Patient login OTP. Valid for {otpExpiryMinutes} minutes.",
                IsVerified = false,
                CreatedDate = DateTime.Now,
                CreatedBy = 0
            };

            _db.TProcessOtps.Add(record);
            await _db.SaveChangesAsync();

            string referenceId = "req-" + record.MsgId.ToString();

            SsSmsConfig? smsConfig = await _db.SsSmsConfigs.AsNoTracking().FirstOrDefaultAsync(x => true);
            string smsUrl = delivery.SmsEnabled
                ? BuildSmsUrl(smsConfig, request.MobileNumber, otp)
                : string.Empty;
            if (!string.IsNullOrWhiteSpace(smsUrl))
            {
                try
                {
                    using var client = new HttpClient();
                    using var response = await client.GetAsync(smsUrl);
                    _db.TSmsOutgoings.Add(new TSmsOutgoing
                    {
                        MobileNumber = request.MobileNumber,
                        Smsstring = "Your OTP is " + otp,
                        Smstype = "PATIENT_LOGIN_OTP",
                        Smsflag = response.IsSuccessStatusCode ? "SENT" : "FAILED",
                        Smsdate = DateTime.Now,
                        Smsurl = smsUrl,
                        Status = response.IsSuccessStatusCode ? 1 : -1,
                        Retry = 0
                    });
                }
                catch (Exception)
                {
                    _db.TSmsOutgoings.Add(new TSmsOutgoing
                    {
                        MobileNumber = request.MobileNumber,
                        Smsstring = "Your OTP is " + otp,
                        Smstype = "PATIENT_LOGIN_OTP",
                        Smsflag = "FAILED",
                        Smsdate = DateTime.Now,
                        Smsurl = smsUrl,
                        Status = -2,
                        Retry = 0
                    });
                }
                await _db.SaveChangesAsync();
            }

            if (delivery.WhatsAppEnabled && string.IsNullOrWhiteSpace(smsUrl))
            {
                _db.TWhatsAppSmsOutgoings.Add(new TWhatsAppSmsOutgoing
                {
                    MobileNumber = request.MobileNumber,
                    Smsstring = "Your OTP is " + otp,
                    Smstype = "PATIENT_LOGIN_OTP",
                    Smsflag = "QUEUED",
                    Smsdate = DateTime.Now,
                    IsSent = false,
                    Status = 0,
                    Retry = 0,
                    CreatedOn = DateTime.Now
                });
                await _db.SaveChangesAsync();
            }

            return Ok(new PatientOtpStatusResponse
            {
                Status = "success",
                Message = "OTP sent successfully",
                ReferenceId = referenceId
            });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyPatientOtpRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new PatientOtpStatusResponse { Status = "error", Message = "Invalid OTP or OTP expired" });

            if (!long.TryParse(request.ReferenceId.StartsWith("req-") ? request.ReferenceId.Substring(4) : "0", out long msgId) ||
                !long.TryParse(request.MobileNumber, out long mobile))
                return BadRequest(new PatientOtpStatusResponse { Status = "error", Message = "Invalid OTP or OTP expired" });

            int otpExpiryMinutes = _configuration.GetValue("PatientOtp:OtpExpiryMinutes", 5);
            if (otpExpiryMinutes < 1) otpExpiryMinutes = 5;
            DateTime expiresAfter = DateTime.Now.AddMinutes(-otpExpiryMinutes);
            var otpRecord = await _db.TProcessOtps
                .FirstOrDefaultAsync(x => x.MsgId == msgId &&
                                          x.MobileNo == mobile &&
                                          x.Otpno == request.Otp &&
                                          x.IsVerified != true &&
                                          x.CreatedDate >= expiresAfter);

            if (otpRecord == null)
                return BadRequest(new PatientOtpStatusResponse { Status = "error", Message = "Invalid OTP or OTP expired" });

            otpRecord.IsVerified = true;
            otpRecord.ModifiedDate = DateTime.Now;

            var patient = await _db.SsMobileAppLogins
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerMobileNo == request.MobileNumber);

            var registration = await _db.VwPatientRegistrationInfos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MobileNo == request.MobileNumber);

            await _db.SaveChangesAsync();

            if (patient == null)
                return BadRequest(new PatientOtpStatusResponse { Status = "error", Message = "Invalid OTP or OTP expired" });

            var profile = new
            {
                customerId = patient.CustomerId,
                customerName = registration?.PatientName ?? patient.CustomerName,
                mobileNumber = patient.CustomerMobileNo,
                address = registration?.Address ?? patient.CustomerAddress,
                email = registration?.EmailId,
                regId = registration?.RegId,
                regNo = registration?.RegNo,
                dateOfBirth = registration?.DateofBirth,
                gender = registration?.GenderName,
                pinCode = registration?.PinNo
            };

            string token = GeneratePatientToken(patient, profile);
            return Ok(new PatientOtpStatusResponse
            {
                Status = "success",
                Message = "Login successful",
                Token = token,
                Patient = profile
            });
        }

        private string GeneratePatientToken(SsMobileAppLogin patient, object profile)
        {
            if (!_loginKeyProvider.IsConfigured)
                throw new InvalidOperationException(_loginKeyProvider.ConfigurationError ?? "Patient OTP JWT is not configured.");

            LoginV2Key key = _loginKeyProvider.Current;
            string issuer = _configuration["AuthenticationSettings:Issuer"] ?? string.Empty;
            string audience = _configuration["AuthenticationSettings:Audience"] ?? string.Empty;
            int configuredDays = _configuration.GetValue("LoginV2:MobileSessionDays", 90);
            int mobileSessionDays = Math.Clamp(configuredDays, 90, 180);
            int minutes = mobileSessionDays * 24 * 60;

            var rsaKey = new RsaSecurityKey(key.Rsa) { KeyId = key.Kid };
            var claims = new[]
            {
                new Claim("sub", patient.CustomerId.ToString()),
                new Claim("patientId", patient.CustomerId.ToString()),
                new Claim("mobile", patient.CustomerMobileNo),
                new Claim("authType", "PatientOtp"),
                new Claim("name", patient.CustomerName)
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256)
            };

            var handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(handler.CreateToken(descriptor));
        }

        private async Task<(bool SmsEnabled, bool WhatsAppEnabled)> ResolveOtpDeliveryAsync()
        {
            string? smsSetting = _configuration["PatientOtp:EnableSms"];
            string? whatsappSetting = _configuration["PatientOtp:EnableWhatsApp"];

            bool smsEnabled = bool.TryParse(smsSetting, out bool configuredSms)
                ? configuredSms
                : await _db.MSmsModuleWiseConfigurations.AsNoTracking().AnyAsync(x =>
                    x.IsActive == true &&
                    x.IsSms &&
                    (x.ModuleName.Contains("PATIENT", StringComparison.OrdinalIgnoreCase) ||
                     x.ModuleName.Contains("OTP", StringComparison.OrdinalIgnoreCase)));

            bool whatsappEnabled = bool.TryParse(whatsappSetting, out bool configuredWhatsapp)
                ? configuredWhatsapp
                : await _db.MSmsModuleWiseConfigurations.AsNoTracking().AnyAsync(x =>
                    x.IsActive == true &&
                    x.IsWhatsApp &&
                    (x.ModuleName.Contains("PATIENT", StringComparison.OrdinalIgnoreCase) ||
                     x.ModuleName.Contains("OTP", StringComparison.OrdinalIgnoreCase)));

            return (smsEnabled, whatsappEnabled);
        }

        private static string BuildSmsUrl(SsSmsConfig? config, string mobile, string otp)
        {
            if (config == null || string.IsNullOrWhiteSpace(config.Url))
                return string.Empty;

            string message = Uri.EscapeDataString("Your OTP is " + otp);
            string url = config.Url
                .Replace("{mobile}", Uri.EscapeDataString(mobile), StringComparison.OrdinalIgnoreCase)
                .Replace("{mobile_number}", Uri.EscapeDataString(mobile), StringComparison.OrdinalIgnoreCase)
                .Replace("{otp}", Uri.EscapeDataString(otp), StringComparison.OrdinalIgnoreCase)
                .Replace("{message}", message, StringComparison.OrdinalIgnoreCase)
                .Replace("{username}", Uri.EscapeDataString(config.UserName ?? ""), StringComparison.OrdinalIgnoreCase)
                .Replace("{password}", Uri.EscapeDataString(config.Spassword ?? ""), StringComparison.OrdinalIgnoreCase)
                .Replace("{sender_id}", Uri.EscapeDataString(config.SenderId ?? ""), StringComparison.OrdinalIgnoreCase)
                .Replace("{campaign}", Uri.EscapeDataString(config.Campaign ?? ""), StringComparison.OrdinalIgnoreCase)
                .Replace("{keys}", Uri.EscapeDataString(config.Keys ?? ""), StringComparison.OrdinalIgnoreCase)
                .Replace("{route_id}", Uri.EscapeDataString(config.Routeid?.ToString() ?? ""), StringComparison.OrdinalIgnoreCase);

            if (url.Contains('{') || url.Contains('}'))
                return string.Empty;

            return url;
        }
    }
}
