using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MConsentMaster
    {
        public long ConsentId { get; set; }
        public long? DepartmentId { get; set; }
        public string? ConsentName { get; set; }
        public string? ConsentDesc { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
