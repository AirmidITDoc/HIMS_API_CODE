using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class ItemWiseStockListDto
    {
        public long StockId { get; set; }
        public long ItemId { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public decimal UnitMRP { get; set; }
        public decimal LandedRate { get; set; }
        public decimal PurUnitRateWF { get; set; }
        public float BalanceQty { get; set; }
        public decimal VatPercentage { get; set; }
        public long BarCodeSeqNo { get; set; }
        public long BatchEdit { get; set; }
        public long ExpDateEdit { get; set; }
        public string ConversionFactor { get; set; }      
        public string? ItemName { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }
        public float? GSTPer { get; set; }







    }
}
