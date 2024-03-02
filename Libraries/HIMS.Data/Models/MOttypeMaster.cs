using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOttypeMaster
    {
        public long OttypeId { get; set; }
        public string? TypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
