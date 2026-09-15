using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDietCategoryMaster
    {
        public long DietCategoryId { get; set; }
        public string CategoryCode { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
