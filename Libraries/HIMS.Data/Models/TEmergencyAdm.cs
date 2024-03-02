using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TEmergencyAdm
    {
        public long EmgId { get; set; }
        public long? RegId { get; set; }
        public DateTime? EmgDate { get; set; }
        public DateTime? EmgTime { get; set; }
        public string? SeqNo { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
    }
}
