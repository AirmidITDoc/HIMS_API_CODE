using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDietMenuDetailMaster
    {
        public long MenuDetId { get; set; }
        public long DietMenuId { get; set; }
        public long FoodItemId { get; set; }
        public int? Quantity { get; set; }
        public long? UnitId { get; set; }
        public int? SequenceNo { get; set; }
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual MDietMenuMaster DietMenu { get; set; } = null!;
    }
}
