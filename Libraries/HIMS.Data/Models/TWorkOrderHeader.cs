using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TWorkOrderHeader
    {
        public long Woid { get; set; }
        public string? Wono { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Remark { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelDate { get; set; }
        public bool IsClosed { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
