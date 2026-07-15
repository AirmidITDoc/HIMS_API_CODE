using System.Text.Json.Serialization;

namespace HIMS.ABHA.Models
{
    // ===== Session / Token =====
    public class SessionTokenRequest
    {
        [JsonPropertyName("clientId")]
        public string ClientId { get; set; } = string.Empty;

        [JsonPropertyName("clientSecret")]
        public string ClientSecret { get; set; } = string.Empty;

        [JsonPropertyName("grantType")]
        public string GrantType { get; set; } = "client_credentials";
    }

    public class SessionTokenResponse
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("tokenType")]
        public string TokenType { get; set; } = string.Empty;

        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refreshExpiresIn")]
        public int RefreshExpiresIn { get; set; }

        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
    }

    public class CertificateDto
    {
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; } = string.Empty;

        [JsonPropertyName("encryptionAlgorithm")]
        public string EncryptionAlgorithm { get; set; } = string.Empty;
    }

    // ===== Aadhaar OTP =====
    public class AadhaarOtpRequest
    {
        [JsonPropertyName("txnId")]
        public string TxnId { get; set; } = string.Empty;

        [JsonPropertyName("scope")]
        public List<string> Scope { get; set; } = new() { "abha-enrol" };

        [JsonPropertyName("loginHint")]
        public string LoginHint { get; set; } = "aadhaar";

        [JsonPropertyName("loginId")]
        public string LoginId { get; set; } = string.Empty; // Encrypted Aadhaar

        [JsonPropertyName("otpSystem")]
        public string OtpSystem { get; set; } = "aadhaar";
    }

    public class OtpResponse
    {
        [JsonPropertyName("txnId")]
        public string TxnId { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }

    public class VerifyUserRequest
    {
        public string ABHANumber { get; set; } = string.Empty;
        public string txnId { get; set; } = string.Empty;
    }
    public class VerifyUserResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; set; } = 0;
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;
        [JsonPropertyName("refreshExpiresIn")]
        public int RefreshExpiresIn { get; set; } = 0;
    }

    public class AadhaarOtpVerifyRequest
    {
        [JsonPropertyName("authData")]
        public AuthData AuthData { get; set; } = new();

        [JsonPropertyName("consent")]
        public Consent Consent { get; set; } = new();
    }

    public class AuthData
    {
        [JsonPropertyName("authMethods")]
        public List<string> AuthMethods { get; set; } = new() { "otp" };

        [JsonPropertyName("otp")]
        public OtpData Otp { get; set; } = new();
    }

    public class OtpData
    {
        [JsonPropertyName("timeStamp")]
        public string TimeStamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

        [JsonPropertyName("txnId")]
        public string TxnId { get; set; } = string.Empty;

        [JsonPropertyName("otpValue")]
        public string OtpValue { get; set; } = string.Empty; // Encrypted

        [JsonPropertyName("mobile")]
        public string? Mobile { get; set; }
    }

    public class Consent
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = "abha-enrollment";

        [JsonPropertyName("version")]
        public string Version { get; set; } = "1.4";
    }

    // ===== OTP =====
    public class OtpRequest
    {
        [JsonPropertyName("scope")]
        public List<string> Scope { get; set; } = new() { "abha-login", "aadhaar-verify" };

        [JsonPropertyName("loginHint")]
        public string LoginHint { get; set; } = "aadhaar";

        [JsonPropertyName("loginId")]
        public string LoginId { get; set; } = string.Empty; // Encrypted mobile

        [JsonPropertyName("otpSystem")]
        public string OtpSystem { get; set; } = "aadhaar";

        [JsonPropertyName("txnId")]
        public string? TxnId { get; set; } = string.Empty;
    }
    public class Account
    {
        public string ABHANumber { get; set; }
        public string preferredAbhaAddress { get; set; }
        public string name { get; set; }
        public string status { get; set; }
    }

    public class VerifyOtpResponse
    {
        public string txnId { get; set; }
        public string authResult { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public int expiresIn { get; set; }
        public string refreshToken { get; set; }
        public int refreshExpiresIn { get; set; }
        public List<Account> accounts { get; set; }
    }

    public class VerifyAddressOtpResponse
    {
        public string AuthResult { get; set; }
        public string Message { get; set; }

        public List<Account> Users { get; set; }

        public TokenInfo Tokens { get; set; }
    }

    // ===== ABHA Profile / Enrolment response =====
    public class AbhaEnrolmentResponse
    {
        [JsonPropertyName("txnId")]
        public string TxnId { get; set; } = string.Empty;

        [JsonPropertyName("tokens")]
        public TokenInfo? Tokens { get; set; }

        [JsonPropertyName("abhaProfile")]
        public AbhaProfile? AbhaProfile { get; set; }

        [JsonPropertyName("isNew")]
        public bool IsNew { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    public class TokenInfo
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("refreshExpiresIn")]
        public int RefreshExpiresIn { get; set; }
    }
    public class LocalizedDetails
    {
        public string name { get; set; } = string.Empty;
        public string stateName { get; set; } = string.Empty;
        public string districtName { get; set; } = string.Empty;
        public string villageName { get; set; } = string.Empty;
        public string townName { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public LocalizedLabels localizedLabels { get; set; } = new LocalizedLabels();
    }

    public class LocalizedLabels
    {
        public string name { get; set; } = string.Empty;
        public string abhaNumber { get; set; } = string.Empty;
        public string abhaAddress { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public string dob { get; set; } = string.Empty;
        public string mobile { get; set; } = string.Empty;
    }

    public class AbhaProfile
    {
        public string ABHANumber { get; set; } = string.Empty;
        public string preferredAbhaAddress { get; set; } = string.Empty;
        public string mobile { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string middleName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string yearOfBirth { get; set; } = string.Empty;
        public string dayOfBirth { get; set; } = string.Empty;
        public string monthOfBirth { get; set; } = string.Empty;
        public string gender { get; set; } = string.Empty;
        public string profilePhoto { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string stateCode { get; set; } = string.Empty;
        public string districtCode { get; set; } = string.Empty;
        public string pincode { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string kycPhoto { get; set; } = string.Empty;
        public string stateName { get; set; } = string.Empty;
        public string districtName { get; set; } = string.Empty;
        public string subdistrictName { get; set; } = string.Empty;
        public List<string> authMethods { get; set; } = new List<string>();
        //public Tags tags { get; set; }
        public bool kycVerified { get; set; }
        public string verificationStatus { get; set; } = string.Empty;
        public string verificationType { get; set; } = string.Empty;
        public LocalizedDetails localizedDetails { get; set; } = new LocalizedDetails();
        public string createdDate { get; set; } = string.Empty;
        public string[] phrAddress { get; set; } = Array.Empty<string>();
    }

    //public class AbhaProfile
    //{
    //    [JsonPropertyName("firstName")]
    //    public string? FirstName { get; set; }

    //    [JsonPropertyName("middleName")]
    //    public string? MiddleName { get; set; }

    //    [JsonPropertyName("lastName")]
    //    public string? LastName { get; set; }

    //    [JsonPropertyName("dob")]
    //    public string? Dob { get; set; }

    //    [JsonPropertyName("gender")]
    //    public string? Gender { get; set; }

    //    [JsonPropertyName("photo")]
    //    public string? Photo { get; set; }

    //    [JsonPropertyName("mobile")]
    //    public string? Mobile { get; set; }

    //    [JsonPropertyName("email")]
    //    public string? Email { get; set; }

    //    [JsonPropertyName("ABHANumber")]
    //    public string? AbhaNumber { get; set; }

    //    [JsonPropertyName("phrAddress")]
    //    public List<string>? PhrAddress { get; set; }

    //    [JsonPropertyName("address")]
    //    public string? Address { get; set; }

    //    [JsonPropertyName("districtCode")]
    //    public string? DistrictCode { get; set; }

    //    [JsonPropertyName("stateCode")]
    //    public string? StateCode { get; set; }

    //    [JsonPropertyName("pinCode")]
    //    public string? PinCode { get; set; }
    //}

    // ===== fins ABHA Address =====
    public class FindAbhaResponse
    {
        [JsonPropertyName("txnId")]
        public string TxnId { get; set; }

        [JsonPropertyName("ABHA")]
        public List<AbhaDetails> ABHA { get; set; }
    }

    public class AbhaDetails
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("ABHANumber")]
        public string ABHANumber { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        // JSON returns "true"/"false" as a string, so keep it as string
        [JsonPropertyName("kycVerified")]
        public string KycVerified { get; set; }

        [JsonPropertyName("authMethods")]
        public List<string> AuthMethods { get; set; }
    }

    // ===== ABHA Address =====
    public class AbhaAddressSuggestionResponse
    {
        [JsonPropertyName("txnId")]
        public string TxnId { get; set; } = string.Empty;

        [JsonPropertyName("abhaAddressList")]
        public List<string> AbhaAddressList { get; set; } = new();
    }

    public class AbhaProfileResponse
    {
        [JsonPropertyName("healthIdNumber")]
        public string HealthIdNumber { get; set; } = string.Empty;

        [JsonPropertyName("abhaAddress")]
        public string AbhaAddress { get; set; } = string.Empty;

        [JsonPropertyName("authMethods")]
        public List<string> AuthMethods { get; set; } = new();

        [JsonPropertyName("blockedAuthMethods")]
        public List<string> BlockedAuthMethods { get; set; } = new();

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; } = string.Empty;
    }

    public class CreateAbhaAddressRequest
    {
        [JsonPropertyName("txnId")]
        public string TxnId { get; set; } = string.Empty;

        [JsonPropertyName("abhaAddress")]
        public string AbhaAddress { get; set; } = string.Empty;

        [JsonPropertyName("preferred")]
        public int Preferred { get; set; } = 1;
    }

    // ===== Generic API result wrapper =====
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public int StatusCode { get; set; }
    }

    // ===== Public Certificate response =====
    public class PublicCertificateResponse
    {
        public string PublicKey { get; set; } = string.Empty;
    }
    public class AbdmErrorResponse
    {
        public AbdmError Error { get; set; }
    }

    public class AbdmError
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }


    // DTOs for controller
    public class AadhaarOtpDto
    {
        public string AadhaarNumber { get; set; } = string.Empty;
        public int OtpType { get; set; } = 1;
    }
    public class VerifyOtpDto
    {
        public string TxnId { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public int OtpType { get; set; } = 1;
    }
    public class CreateAddressDto
    {
        public string TxnId { get; set; } = string.Empty;
        public string AbhaAddress { get; set; } = string.Empty;
    }
    public class FindAbhaMobileDto
    {
        public string Mobile { get; set; } = string.Empty;
    }
    public class ProfileRequestDto
    {
        public string Token { get; set; } = string.Empty;
        public bool IsAddress { get; set; } = false;
    }
    public class AbhaFindRequestOtpDto
    {
        public string TxnId { get; set; } = string.Empty;
        public string AadhaarNumber { get; set; } = string.Empty;
        public int OtpType { get; set; } = 1;
    }
}
