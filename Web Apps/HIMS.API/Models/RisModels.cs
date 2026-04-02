using System.Text.Json.Serialization;

namespace HIMS.API.Models
{
    // ──────────────── Auth ────────────────
    public class AuthRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public string? access_token { get; set; }
        public string? Error { get; set; }
    }

    // ──────────────── Comment ────────────────
    public class CommentDto
    {
        [JsonPropertyName("comment")]
        public string Comment { get; set; } = string.Empty;
        [JsonPropertyName("comments_by")]
        public string CommentBy { get; set; } = string.Empty;       // Domain ID of the user
        [JsonPropertyName("comment_timestamp")]
        public string CommentTimestamp { get; set; } = string.Empty; // ISO 8601
    }

    // ──────────────── Create Radiology Order ────────────────
    public class CreateRadiologyOrderRequest
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("middle_name")]
        public string MiddleName { get; set; } = string.Empty;       // optional
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;         // optional
        [JsonPropertyName("patient_id")]
        public string PatientId { get; set; } = string.Empty;
        [JsonPropertyName("patient_dob")]
        public string PatientDob { get; set; } = string.Empty;       // dd-mm-YYYY; optional if age given
        [JsonPropertyName("patient_age")]
        public string PatientAge { get; set; } = string.Empty;       // optional if dob given
        [JsonPropertyName("patient_gender")]
        public string PatientGender { get; set; } = string.Empty;
        [JsonPropertyName("patient_phone_number")]
        public string PatientPhoneNumber { get; set; } = string.Empty;
        [JsonPropertyName("patient_country_code")]
        public string PatientCountryCode { get; set; } = string.Empty;
        [JsonPropertyName("patient_email")]
        public string PatientEmail { get; set; } = string.Empty;     // optional
        [JsonPropertyName("modality")]
        public string Modality { get; set; } = string.Empty;
        [JsonPropertyName("accession_number")]
        public string AccessionNumber { get; set; } = string.Empty;  // unique order ID
        [JsonPropertyName("scan_desc")]
        public string ScanDesc { get; set; } = string.Empty;
        [JsonPropertyName("scan_id")]
        public string ScanId { get; set; } = string.Empty;           // optional
        [JsonPropertyName("ref_physician")]
        public string RefPhysician { get; set; } = string.Empty;     // optional
        [JsonPropertyName("ref_physician_phone_number")]
        public string RefPhysicianPhoneNumber { get; set; } = string.Empty; // optional
        [JsonPropertyName("ref_country_code")]
        public string RefCountryCode { get; set; } = string.Empty;
        [JsonPropertyName("ref_physician_email")]
        public string RefPhysicianEmail { get; set; } = string.Empty; // optional
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; } = string.Empty;       // optional
        [JsonPropertyName("branch_code")]
        public string BranchCode { get; set; } = string.Empty;       // optional
        [JsonPropertyName("branch_name")]
        public string BranchName { get; set; } = string.Empty;       // optional
        [JsonPropertyName("package")]
        public string Package { get; set; } = string.Empty;
        [JsonPropertyName("appointment_date_time")]
        public string AppointmentDateTime { get; set; } = string.Empty; // ISO 8601
        [JsonPropertyName("comments")]
        public List<CommentDto> Comments { get; set; } = new();
    }

    // ──────────────── Update Radiology Order ────────────────
    public class UpdateRadiologyOrderRequest
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("middle_name")]
        public string MiddleName { get; set; } = string.Empty;
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("patient_dob")]
        public string PatientDob { get; set; } = string.Empty;
        [JsonPropertyName("patient_age")]
        public string PatientAge { get; set; } = string.Empty;
        [JsonPropertyName("patient_gender")]
        public string PatientGender { get; set; } = string.Empty;
        [JsonPropertyName("patient_phone_number")]
        public string PatientPhoneNumber { get; set; } = string.Empty;
        [JsonPropertyName("patient_email")]
        public string PatientEmail { get; set; } = string.Empty;
        [JsonPropertyName("modality")]
        public string Modality { get; set; } = string.Empty;
        [JsonPropertyName("scan_desc")]
        public string ScanDesc { get; set; } = string.Empty;
        [JsonPropertyName("scan_id")]
        public string ScanId { get; set; } = string.Empty;
        [JsonPropertyName("ref_physician")]
        public string RefPhysician { get; set; } = string.Empty;
        [JsonPropertyName("ref_physician_phone_number")]
        public string RefPhysicianPhoneNumber { get; set; } = string.Empty;
        [JsonPropertyName("ref_physician_email")]
        public string RefPhysicianEmail { get; set; } = string.Empty;
        [JsonPropertyName("comments")]
        public List<CommentDto> Comments { get; set; } = new();
    }

    // ──────────────── Delete Radiology Order ────────────────
    public class DeleteRadiologyOrderRequest
    {
        [JsonPropertyName("accession_number")]
        public string AccessionNumber { get; set; } = string.Empty;
        [JsonPropertyName("scan_id")]
        public string ScanId { get; set; } = string.Empty;
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; } = string.Empty;
    }

    // ──────────────── Send Report Details (HMIS implements this) ────────────────
    public class ReportDetailsRequest
    {
        public string PatientName { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string Modality { get; set; } = string.Empty;
        public string AccessionNumber { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string ScanDesc { get; set; } = string.Empty;
        public string? ScanLink { get; set; }
        public string? ReportLink { get; set; }
        public string? Base64PdfString { get; set; }
    }

    // ──────────────── Send Report Share Status (HMIS implements this) ────────────────
    public class ReportShareStatusRequest
    {
        public string PatientName { get; set; } = string.Empty;
        public string PatientId { get; set; } = string.Empty;
        public string Modality { get; set; } = string.Empty;
        public string AccessionNumber { get; set; } = string.Empty;
        public string ExternalId { get; set; } = string.Empty;
        public string ShareMode { get; set; } = string.Empty;     // email/sms/whatsapp
        public bool ShareWithPatient { get; set; }
        public bool ShareWithRefPhy { get; set; }
        public string ScanDesc { get; set; } = string.Empty;
        public string? ScanLink { get; set; }
        public string? ReportLink { get; set; }
        public string? Base64PdfString { get; set; }
    }

    // ──────────────── Receive Patient History ────────────────
    public class PatientHistoryFileLink
    {
        public string File { get; set; } = string.Empty;        // URL
        public string FileTimestamp { get; set; } = string.Empty; // ISO 8601
    }

    public class PatientHistoryBase64File
    {
        public string Base64String { get; set; } = string.Empty;
        public string FileTimestamp { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;     // with extension
    }

    public class PatientHistoryRequest
    {
        public string ExternalId { get; set; } = string.Empty;
        public List<CommentDto> Comments { get; set; } = new();
        public List<PatientHistoryFileLink> FileLinks { get; set; } = new();
        public List<PatientHistoryBase64File> Base64Files { get; set; } = new();
    }

    // ──────────────── Generic RIS Response ────────────────
    public class RisApiResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
