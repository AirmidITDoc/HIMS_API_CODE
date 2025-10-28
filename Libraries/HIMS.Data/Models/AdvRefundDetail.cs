using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class AdvRefundDetail
    {
        public long AdvRefId { get; set; }
        public double? AdvDetailId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public double? AdvRefundAmt { get; set; }
    }
}
