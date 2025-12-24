using System.Numerics;

namespace HIMS.Data.DTO.Administration
{
    public class PathResultEntryListDto
    {
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? OP_IP_No { get; set; }
        public DateTime? VADate { get; set; }
        public string? VATime { get; set; }
        public string? DoctorName { get; set; }
        public long? Visit_Adm_ID { get; set; }
        public string? PathTestID { get; set; }
        public string? ServiceName { get; set; }
        public long PathReportId { get; set; }
        public long ServiceId { get; set; }
        public string? DOA { get; set; }
        public string? DOT { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public long opdipdtype { get; set; }
        public string opdipdid { get; set; }
        public string? PatientType { get; set; }
        public string? PBillNo { get; set; }
        public string AgeYear { get; set; }
        public string? GenderName { get; set; }
        public long? IsTemplateTest { get; set; }
        public string? CategoryName { get; set; }
        public long ChargeId { get; set; }
        public long? Adm_Visit_docId { get; set; }
        public string? SampleNo { get; set; }
        public string? SampleCollectionTime { get; set; }
        public string? IsSampleCollection { get; set; }
        public bool? IsVerifySign { get; set; }
        public long? PathTestServiceId { get; set; }
        public long? GenderId { get; set; }
        public long? IsVerifyid { get; set; }
        public string? IsVerifyedDate { get; set; }
        public long? OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public DateTime? OutSourceSampleSentDateTime { get; set; }
        public long? OutSourceStatus { get; set; }
        public DateTime? OutSourceReportCollectedDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public DateTime? OutSourceCreatedDateTime { get; set; }
        public long? OutSourceModifiedby { get; set; }
        public DateTime? OutSourceModifiedDateTime { get; set; }
        public bool? IsPathOutSource { get; set; }
        public bool? IsRadOutSource { get; set; }
        public string VerifiedUserName { get; set; }
        public string? ReportDate { get; set; }
        public string? ReportTime { get; set; }
        public string ReportCompletedUser { get; set; }
        public long AddedBy { get; set; }
        public long UpdatedBy { get; set; }

    }
        public class pathologistdoctorDto
        {
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
       
        }
    public class patientTemplateRetriveComboDto
    { 
        public long OtrequestId { get; set; }
        public long DoctorTypeId { get; set; }
        public long DoctorType { get; set; }
        public string? DoctorId { get; set; }
        public string? DoctorName { get; set; }

    }
  
}

