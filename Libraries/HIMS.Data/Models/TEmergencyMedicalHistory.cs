using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TEmergencyMedicalHistory
    {
        public long EmgHistoryId { get; set; }
        public long? EmgId { get; set; }
        public string? Height { get; set; }
        public string? Pweight { get; set; }
        public string? Bmi { get; set; }
        public string? Bsl { get; set; }
        public string? SpO2 { get; set; }
        public string? Temp { get; set; }
        public string? Pulse { get; set; }
        public string? Bp { get; set; }
        public string? ChiefComplaint { get; set; }
        public string? Diagnosis { get; set; }
        public string? Examination { get; set; }
        public string? Advice { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
