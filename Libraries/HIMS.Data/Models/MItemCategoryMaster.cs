using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemCategoryMaster
    {
        public long ItemCategoryId { get; set; }
        public string? ItemCategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ItemTypeId { get; set; }
    }
}
