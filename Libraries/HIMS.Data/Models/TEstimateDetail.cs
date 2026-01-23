using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TEstimateDetail
    {
        public long EstimateDetId { get; set; }
        public long? EstimateId { get; set; }
        public long? ServiceId { get; set; }
        public decimal? Price { get; set; }
        public long? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TEstimateHeader? Estimate { get; set; }
    }
}
