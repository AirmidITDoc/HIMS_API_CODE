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
    }
}
