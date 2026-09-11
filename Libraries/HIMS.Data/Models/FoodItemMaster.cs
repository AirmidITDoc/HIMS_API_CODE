using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class FoodItemMaster
    {
        public long FoodItemId { get; set; }
        public string? FoodName { get; set; }
        public string? FoodCode { get; set; }
        public long? FoodCategory { get; set; }
        public string? LocalName { get; set; }
        public long? Unit { get; set; }
        public decimal? ServingSize { get; set; }
        public bool? Vegetarian { get; set; }
        public bool? Active { get; set; }
    }
}
