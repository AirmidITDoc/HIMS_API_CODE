using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MRadiologyCategoryMaster
    {
        public long CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? Isdeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
