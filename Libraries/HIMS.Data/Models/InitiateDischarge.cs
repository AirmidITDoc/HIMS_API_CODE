using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class InitiateDischarge
    {
        public long InitateDiscId { get; set; }
        public long? AdmId { get; set; }
        public string? DepartmentName { get; set; }
        public long? DepartmentId { get; set; }
        public bool? IsApproved { get; set; }
        public long? ApprovedBy { get; set; }
        public DateTime? ApprovedDatetime { get; set; }
        public bool? IsNoDues { get; set; }
        public string? Comments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
