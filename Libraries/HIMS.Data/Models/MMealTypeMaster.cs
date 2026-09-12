using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MMealTypeMaster
    {
        public long MealId { get; set; }
        public string MealTypeCode { get; set; } = null!;
        public string MealName { get; set; } = null!;
        public DateTime? DefaultTime { get; set; }
        public DateTime? OrderCutoffTime { get; set; }
        public DateTime? PreparationStartTime { get; set; }
        public DateTime? DispatchTime { get; set; }
        public int? MealSequence { get; set; }
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
