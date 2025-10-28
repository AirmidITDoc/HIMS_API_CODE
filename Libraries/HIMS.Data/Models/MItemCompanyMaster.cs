using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemCompanyMaster
    {
        public long CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompShortName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
