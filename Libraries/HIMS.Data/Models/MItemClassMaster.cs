using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemClassMaster
    {
        public long ItemClassId { get; set; }
        public string? ItemClassName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
