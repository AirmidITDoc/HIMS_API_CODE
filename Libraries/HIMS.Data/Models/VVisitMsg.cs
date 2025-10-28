using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class VVisitMsg
    {
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? AgeYear { get; set; }
        public string? DoctorName { get; set; }
        public string? Doa { get; set; }
        public string? Dot { get; set; }
        public string? MobileNo { get; set; }
        public long VisitId { get; set; }
        public string DoctorMobileNo { get; set; } = null!;
        public string? GenderName { get; set; }
        public string? RefDoctorName { get; set; }
        public string RefDoctorMobileNo { get; set; } = null!;
    }
}
