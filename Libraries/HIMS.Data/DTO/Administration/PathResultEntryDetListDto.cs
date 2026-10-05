using System.Numerics;
namespace HIMS.Data.DTO.Administration
{
    public class PathResultEntryDetListDto
    {
        public long PathReportID { get; set; }
        public DateTime? PathDate { get; set; }
        public string? DOA { get; set; }
        public string? DOT { get; set; }
        public DateTime? PathTime { get; set; }
        public long? opdipdtype { get; set; }
        public long? opdipdid { get; set; }
        public long? VisitAdmID { get; set; }
        public string? PatientType { get; set; }
        public long? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? OPIPNumber { get; set; }
        public string? DoctorName { get; set; }
        public long? AdmVisitDoctorId { get; set; }
        public long? RefDoctorID { get; set; }
        public string? ReportTime { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? PathTestID { get; set; }
        public long? TestId { get; set; }
        public long? CategoryId { get; set; }
        public string? PathResultDr1 { get; set; }

        public long? IsTemplateTest { get; set; }

        public bool? TestType { get; set; }
        public long? ChargeId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public string? SampleNo { get; set; }
        public DateTime? SampleCollectionTime { get; set; }
        public bool? IsSampleCollection { get; set; }
        public long? SampleCollectedBy { get; set; }
        public string? SuggestionNotes { get; set; }
        public bool? IsVerifySign { get; set; }
        public long? IsVerifyid { get; set; }
        public long? OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public DateTime? OutSourceSampleSentDateTime { get; set; }
        public long? OutSourceStatus { get; set; }
        public DateTime? OutSourceReportCollectedDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public DateTime? OutSourceCreatedDateTime { get; set; }
        public long? OutSourceModifiedby { get; set; }
        public DateTime? OutSourceModifiedDateTime { get; set; }
        public DateTime? SampleReceviedDateTime { get; set; }
        public long? SampleReceviedUserId { get; set; }
        public bool? IsSampleReceivedStatus { get; set; }
        public bool? IsSampleReceviedCancel { get; set; }
        public string? SampleReceviedCancelReason { get; set; }
        public long? SampleReceviedCanceledBy { get; set; }
        public DateTime? SampleReceivedCancelDate { get; set; }
        public int? OrderNo { get; set; }
        public long? UnitId { get; set; }
        public string? VerifiedUser { get; set; }

        //public long? Total_Row { get; set; }

    }
}
