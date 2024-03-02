using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMrpstockAdjestment
    {
        public long StockAdgId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public int? AdDdType { get; set; }
        public float? AdDdQty { get; set; }
        public float? PreBalQty { get; set; }
        public float? AfterBalQty { get; set; }
        public decimal? Mrpadg { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
    }
}
