using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MFoodItemMaster
    {
        public long FoodItemId { get; set; }
        public string FoodCode { get; set; } = null!;
        public string FoodName { get; set; } = null!;
        public long FoodCategoryId { get; set; }
        public string? LocalName { get; set; }
        public long? Unit { get; set; }
        public bool? IsVegetarian { get; set; }
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
