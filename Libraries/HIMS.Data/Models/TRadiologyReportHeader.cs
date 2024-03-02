using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TRadiologyReportHeader
    {
        public long RadReportId { get; set; }
        public DateTime? RadDate { get; set; }
        public DateTime? RadTime { get; set; }
        public long? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? RadTestId { get; set; }
        public long? RadResultDr1 { get; set; }
        public long? RadResultDr2 { get; set; }
        public long? RadResultDr3 { get; set; }
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
        public string? ResultEntry { get; set; }
        public bool? TestType { get; set; }
        public string? SuggestionNotes { get; set; }
        public long? AdmVisitDoctorId { get; set; }
        public long? RefDoctorId { get; set; }
    }
}
