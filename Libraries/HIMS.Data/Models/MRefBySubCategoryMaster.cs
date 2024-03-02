using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MRefBySubCategoryMaster
    {
        public long RefBySubId { get; set; }
        public string? RefBySubName { get; set; }
        public long? RefById { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? MobileNo { get; set; }
    }
}
