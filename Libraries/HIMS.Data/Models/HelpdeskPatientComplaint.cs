using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class HelpdeskPatientComplaint
    {
        public int ComplaintId { get; set; }
        public string PatientName { get; set; } = null!;
        public string RegId { get; set; } = null!;
        public int? OpdipdNo { get; set; }
        public int? DepartmentId { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }
        public string? Complaint { get; set; }
        public DateTime? ComplaintDate { get; set; }
        public DateTime? ComplaintTime { get; set; }
        public bool? IsDischarge { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
