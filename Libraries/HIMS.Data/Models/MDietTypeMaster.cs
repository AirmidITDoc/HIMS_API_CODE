using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDietTypeMaster
    {
        public long DietTypeId { get; set; }
        public string DietCode { get; set; } = null!;
        public string DietName { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string? Description { get; set; }
        public long? DietCategoryId { get; set; }
        public int? DefaultCalories { get; set; }
        public int? DefaultProtein { get; set; }
        public int? DefaultFluid { get; set; }
        public bool? Active { get; set; }
        public int? DisplayOrder { get; set; }
        public string? Remarks { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
