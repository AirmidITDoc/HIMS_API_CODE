using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TempPkCurrStkSale
    {
        public DateTime? Date { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? SalesQty { get; set; }
        public long? StoreId { get; set; }
        public long? StkId { get; set; }
    }
}
