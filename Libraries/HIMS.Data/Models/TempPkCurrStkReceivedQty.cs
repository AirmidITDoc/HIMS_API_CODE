using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TempPkCurrStkReceivedQty
    {
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public DateTime? IssueDate { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? IssueQty { get; set; }
        public decimal? UnitPurRate { get; set; }
        public long? StkId { get; set; }
    }
}
