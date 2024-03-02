using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class OpphAppSmsqueryTempleteSm
    {
        public long PhoneAppId { get; set; }
        public string? AppDate { get; set; }
        public string? SeqNo { get; set; }
        public string? PatientName { get; set; }
        public string? Address { get; set; }
        public string MobileNo { get; set; } = null!;
        public string? PhAppDate { get; set; }
        public string? PhAppTime { get; set; }
        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorMobileNo { get; set; }
        public bool? IsCancelled { get; set; }
        public DateTime? PhoneAppDate { get; set; }
        public string? TodaysDate { get; set; }
    }
}
