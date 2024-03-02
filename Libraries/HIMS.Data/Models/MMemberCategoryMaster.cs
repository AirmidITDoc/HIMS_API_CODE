using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MMemberCategoryMaster
    {
        public long MemberCategoryId { get; set; }
        public string? MemberCategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
