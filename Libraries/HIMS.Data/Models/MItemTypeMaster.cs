using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemTypeMaster
    {
        public long ItemTypeId { get; set; }
        public string? ItemTypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
