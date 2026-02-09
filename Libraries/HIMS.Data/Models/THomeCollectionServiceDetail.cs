using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class THomeCollectionServiceDetail
    {
        public long HomeDetId { get; set; }
        public long? HomeCollectionId { get; set; }
        public long? UnitId { get; set; }
        public long? TestId { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? IsCancel { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual THomeCollectionRegistrationInfo? HomeCollection { get; set; }
    }
}
