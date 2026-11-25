using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HIMS.Api.Models.Login
{
    public sealed class SendPatientOtpRequest
    {
        [RegularExpression(@"^[0-9]{10,15}$")]
        [JsonPropertyName("mobileNo")]
        public string? MobileNo { get; set; }

        [JsonPropertyName("hospital_id")]
        public string? HospitalId { get; set; }

        [RegularExpression(@"^[0-9]{10,15}$")]
        [JsonPropertyName("mobile_number")]
        public string? LegacyMobileNumber { get; set; }

        [JsonIgnore]
        public string MobileNumber =>
            !string.IsNullOrWhiteSpace(MobileNo) ? MobileNo : LegacyMobileNumber ?? string.Empty;
    }

    public sealed class VerifyPatientOtpRequest
    {
        [RegularExpression(@"^[0-9]{10,15}$")]
        [JsonPropertyName("mobileNo")]
        public string? MobileNo { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{4,10}$")]
        [JsonPropertyName("otp")]
        public string Otp { get; set; } = string.Empty;

        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }

        [RegularExpression(@"^[0-9]{10,15}$")]
        [JsonPropertyName("mobile_number")]
        public string? LegacyMobileNumber { get; set; }

        [JsonIgnore]
        public string MobileNumber =>
            !string.IsNullOrWhiteSpace(MobileNo) ? MobileNo : LegacyMobileNumber ?? string.Empty;
    }

    public sealed class PatientOtpStatusResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("reference_id")]
        public string? ReferenceId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("patient")]
        public object? Patient { get; set; }
    }
}
