﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class DayWiseCurrentStockDto
    {
        public string ItemName { get; set; }
        public long StockId { get; set; }
        public long StoreId {  get; set; }
        public long ItemId { get; set; }
        public float? OpeningBalance { get; set; }
        public float? ReceivedQty { get; set; }
        public float? IssueQty { get; set; }
        public float? BalanceQty { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? VatPercentage { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }
        public decimal? PurUnitRate { get; set; }
        public decimal? PurUnitRateWf { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }
        public long? BarCodeSeqNo { get; set; }
        public long? IstkId { get; set; }
        public float? GrnRetQty { get; set; }
        public DateTime? LedgerDate { get; set; }
        public float? ClosingBalance { get; set; }


    }
}
