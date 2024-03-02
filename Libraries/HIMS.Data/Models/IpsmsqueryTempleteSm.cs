using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class IpsmsqueryTempleteSm
    {
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? MobileNo { get; set; }
        public long RegId { get; set; }
        public string? AgeYear { get; set; }
        public string? DoctorName { get; set; }
        public string RefDoctorName { get; set; } = null!;
        public int OpdIpdType { get; set; }
        public int NetPayableAmt { get; set; }
        public int BillNo { get; set; }
        public string? DoctorMobileNo { get; set; }
        public string? RefDoctorMobileNo { get; set; }
        public string? TodaysDate { get; set; }
        public string? Ipdno { get; set; }
        public long AdmissionId { get; set; }
        public long? DischargeId { get; set; }
        public string? Doadatetime { get; set; }
        public string? Doddatetime { get; set; }
        public string? WardName { get; set; }
        public string? BedName { get; set; }
    }
}
