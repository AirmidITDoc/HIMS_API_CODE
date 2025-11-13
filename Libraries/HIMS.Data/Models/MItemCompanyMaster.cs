using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemCompanyMaster
    {
        public long CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompShortName { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
