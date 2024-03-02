using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPhoneAppointment
    {
        public long PhoneAppId { get; set; }
        public DateTime? AppDate { get; set; }
        public DateTime? AppTime { get; set; }
        public string? SeqNo { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? PhAppDate { get; set; }
        public DateTime? PhAppTime { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public string? RegNo { get; set; }
    }
}
