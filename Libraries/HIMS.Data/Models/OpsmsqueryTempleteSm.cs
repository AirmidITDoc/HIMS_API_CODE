using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class OpsmsqueryTempleteSm
    {
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? MobileNo { get; set; }
        public long RegId { get; set; }
        public string? AgeYear { get; set; }
        public string? DoctorName { get; set; }
        public string RefDoctorName { get; set; } = null!;
        public string? Opdno { get; set; }
        public string? DoctorMobileNo { get; set; }
        public string? RefDoctorMobileNo { get; set; }
        public long VisitId { get; set; }
        public int PbillNo { get; set; }
        public int OpdIpdType { get; set; }
        public int NetPayableAmt { get; set; }
        public int BillNo { get; set; }
        public string? TodaysDate { get; set; }
    }
}
