﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCurrentStock2322
    {
        public TCurrentStock2322()
        {
            StockLogs = new HashSet<StockLog>();
        }

        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public float? OpeningBalance { get; set; }
        public float? ReceivedQty { get; set; }
        public float? IssueQty { get; set; }
        public float? BalanceQty { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? VatPercentage { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? PurUnitRate { get; set; }
        public decimal? PurUnitRateWf { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }
        public long? BarCodeSeqNo { get; set; }
        public long? IstkId { get; set; }
        public float? GrnRetQty { get; set; }
        public float? IssDeptQty { get; set; }

        public virtual ICollection<StockLog> StockLogs { get; set; }
    }
}
