using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.GRN
{
    public class GRNReturnWithoutGRNBySupplierIdDto
    {
        public long Grnid { get; set; }
        public string? GrnNumber { get; set; }
        public DateTime? Grndate { get; set; }
        public DateTime? Grntime { get; set; }
        public long? StoreId { get; set; }
        public string? ItemName { get; set; }
        public float? ReceiveQty { get; set; }
        public float? FreeQty { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public long? ConversionFactor { get; set; }
        public double? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public double? DiscPercentage { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public float? TotalQty { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public double? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public double? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public double? ReturnQty { get; set; }
        public float? BalanceQty { get; set; }
        public long? StkId { get; set; }
        public long StockId { get; set; }
        public long ItemId { get; set; }



    }
}
