using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwPatientListAdmission
    {
        public string? PatientName { get; set; }
        public string? Address { get; set; }
        public string? GenderName { get; set; }
        public string? AgeYear { get; set; }
        public string? DepartmentName { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public string? Ipdno { get; set; }
        public long? IsCancelled { get; set; }
        public decimal? PaidAmount { get; set; }
    }
}
