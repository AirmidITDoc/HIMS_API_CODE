using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MFoodCategoryMaster
    {
        public long FoodCategoryId { get; set; }
        public string FoodCategoryCode { get; set; } = null!;
        public string FoodCategoryName { get; set; } = null!;
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
