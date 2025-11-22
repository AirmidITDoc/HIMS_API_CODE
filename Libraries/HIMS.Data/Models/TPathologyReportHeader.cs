using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPathologyReportHeader
    {
        public long PathReportId { get; set; }
        public DateTime? PathDate { get; set; }
        public DateTime? PathTime { get; set; }
        public long? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? ReportTime { get; set; }
        public long? PathTestId { get; set; }
        public long? PathResultDr1 { get; set; }
        public long? PathResultDr2 { get; set; }
        public long? PathResultDr3 { get; set; }
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
        public long? AdmVisitDoctorId { get; set; }
        public long? RefDoctorId { get; set; }
        public bool? IsVerifySign { get; set; }
        public long? IsVerifyid { get; set; }
        public DateTime? IsVerifyedDate { get; set; }
        public long? OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public DateTime? OutSourceSampleSentDateTime { get; set; }
        public long? OutSourceStatus { get; set; }
        public DateTime? OutSourceReportCollectedDateTime { get; set; }
        public long? OutSourceCreatedBy { get; set; }
        public DateTime? OutSourceCreatedDateTime { get; set; }
        public long? OutSourceModifiedby { get; set; }
        public DateTime? OutSourceModifiedDateTime { get; set; }
        public long? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? Opipnumber { get; set; }
        public string? DoctorName { get; set; }
    }
}
