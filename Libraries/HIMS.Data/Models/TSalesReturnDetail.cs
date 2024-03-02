using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TSalesReturnDetail
    {
        public long SalesReturnDetId { get; set; }
        public long? SalesReturnId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? UnitMrp { get; set; }
        public double? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? VatPer { get; set; }
        public decimal? VatAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? LandedPrice { get; set; }
        public decimal? TotalLandedAmount { get; set; }
        public decimal? PurRate { get; set; }
        public decimal? PurTot { get; set; }
        public long? SalesId { get; set; }
        public long? SalesDetId { get; set; }
        public byte? IsCashOrCredit { get; set; }
        public float? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public float? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public float? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public long? StkId { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? Mrptotal { get; set; }
    }
}
