using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TStockUpdate
    {
        public long StockId { get; set; }
        public long? ItemId { get; set; }
        public double? ReceivedQty { get; set; }
        public double? IssueQty { get; set; }
        public double? BalQty { get; set; }
    }
}
