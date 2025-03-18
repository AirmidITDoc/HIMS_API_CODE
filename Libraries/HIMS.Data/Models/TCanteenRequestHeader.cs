using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCanteenRequestHeader
    {
        public long ReqId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string? ReqNo { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public long? WardId { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public bool? IsBillGenerated { get; set; }
        public bool? IsPrint { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
