using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MFoodPreferenceMaster
    {
        public long FoodPreferenceId { get; set; }
        public string FoodPreferenceCode { get; set; } = null!;
        public string FoodPreferenceName { get; set; } = null!;
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
