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
        public string? AccessToken { get; set; }
        public string? Description { get; set; }
        public string? Error { get; set; }
        public int? StatusCode { get; set; }
    }

    // ──────────────── Comment ────────────────
    public class CommentDto
    {
        public string Comment { get; set; } = string.Empty;
        public string CommentBy { get; set; } = string.Empty;       // Domain ID of the user
        public string CommentTimestamp { get; set; } = string.Empty; // ISO 8601
    }

    // ──────────────── Create Radiology Order ────────────────
    public class CreateRadiologyOrderRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;       // optional
        public string LastName { get; set; } = string.Empty;         // optional
        public string PatientId { get; set; } = string.Empty;
        public string PatientDob { get; set; } = string.Empty;       // dd-mm-YYYY; optional if age given
        public string PatientAge { get; set; } = string.Empty;       // optional if dob given
        public string PatientGender { get; set; } = string.Empty;
        public string PatientPhoneNumber { get; set; } = string.Empty;
        public string PatientCountryCode { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;     // optional
        public string Modality { get; set; } = string.Empty;
        public string AccessionNumber { get; set; } = string.Empty;  // unique order ID
        public string ScanDesc { get; set; } = string.Empty;
        public string ScanId { get; set; } = string.Empty;           // optional
        public string RefPhysician { get; set; } = string.Empty;     // optional
        public string RefPhysicianPhoneNumber { get; set; } = string.Empty; // optional
        public string RefCountryCode { get; set; } = string.Empty;
        public string RefPhysicianEmail { get; set; } = string.Empty; // optional
        public string ExternalId { get; set; } = string.Empty;       // optional
        public string BranchCode { get; set; } = string.Empty;       // optional
        public string BranchName { get; set; } = string.Empty;       // optional
        public string Package { get; set; } = string.Empty;
        public string AppointmentDateTime { get; set; } = string.Empty; // ISO 8601
        public List<CommentDto> Comments { get; set; } = new();
    }

    // ──────────────── Update Radiology Order ────────────────
    public class UpdateRadiologyOrderRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PatientDob { get; set; } = string.Empty;
        public string PatientAge { get; set; } = string.Empty;
        public string PatientGender { get; set; } = string.Empty;
        public string PatientPhoneNumber { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;
        public string Modality { get; set; } = string.Empty;
        public string ScanDesc { get; set; } = string.Empty;
        public string ScanId { get; set; } = string.Empty;
        public string RefPhysician { get; set; } = string.Empty;
        public string RefPhysicianPhoneNumber { get; set; } = string.Empty;
        public string RefPhysicianEmail { get; set; } = string.Empty;
        public List<CommentDto> Comments { get; set; } = new();
    }

    // ──────────────── Delete Radiology Order ────────────────
    public class DeleteRadiologyOrderRequest
    {
        public string AccessionNumber { get; set; } = string.Empty;
        public string ScanId { get; set; } = string.Empty;
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
