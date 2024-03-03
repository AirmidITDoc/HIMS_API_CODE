using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DbPrefixMaster
    {
        public long PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public long? SexId { get; set; }
        public long? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
