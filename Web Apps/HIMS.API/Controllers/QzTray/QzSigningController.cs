using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.API.Controllers.QzTray
{
    [ApiController]
    [Route("api/qz")]
    public class QzSigningController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public QzSigningController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class SignRequestModel
        {
            public string Request { get; set; } = string.Empty;
        }

        [HttpPost("sign-message")]
        public IActionResult SignMessage([FromBody] SignRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model?.Request))
            {
                return BadRequest("Invalid request string.");
            }

            try
            {
                // PEM private key
                string keyPath = Path.Combine(_environment.ContentRootPath, "App_Data", "private-key.pem");

                if (!System.IO.File.Exists(keyPath))
                {
                    return NotFound("Private key file not found on server.");
                }

                string pemText = System.IO.File.ReadAllText(keyPath);

                using var rsa = RSA.Create();
                rsa.ImportFromPem(pemText);

                byte[] dataToSign = Encoding.UTF8.GetBytes(model.Request);
                byte[] signatureBytes = rsa.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                // Return the Base64 signature
                string base64Signature = Convert.ToBase64String(signatureBytes);
                return Ok(base64Signature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Signing error: {ex.Message}");
            }
        }
    }
}