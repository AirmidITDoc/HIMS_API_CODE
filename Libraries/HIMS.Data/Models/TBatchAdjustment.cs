using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TBatchAdjustment
    {
        public long BatchAdjId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? OldBatchNo { get; set; }
        public DateTime? OldExpDate { get; set; }
        public string? NewBatchNo { get; set; }
        public DateTime? NewExpDate { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? AddedDateTime { get; set; }
    }
}
