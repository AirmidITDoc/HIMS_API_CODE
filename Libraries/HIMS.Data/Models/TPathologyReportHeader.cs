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
        public long? PathTestId { get; set; }
        public long? PathResultDr1 { get; set; }
        public long? PathResultDr2 { get; set; }
        public long? PathResultDr3 { get; set; }
        public long? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ChargeId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsPrinted { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime? ReportTime { get; set; }
        public string? SampleNo { get; set; }
        public DateTime? SampleCollectionTime { get; set; }
        public bool? IsSampleCollection { get; set; }
        public long? IsTemplateTest { get; set; }
        public bool? TestType { get; set; }
        public string? SuggestionNotes { get; set; }
        public long? AdmVisitDoctorId { get; set; }
        public long? RefDoctorId { get; set; }
        public bool? IsVerifySign { get; set; }
        public long? IsVerifyid { get; set; }
        public DateTime? IsVerifyedDate { get; set; }
    }
}
