using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TGrndetail
    {
        public long GrndetId { get; set; }
        public long? Grnid { get; set; }
        public long? ItemId { get; set; }
        public long? Uomid { get; set; }
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
        public double? OtherTax { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public float? TotalQty { get; set; }
        public long? Pono { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? ReturnQty { get; set; }
        public decimal? PurUnitRate { get; set; }
        public decimal? PurUnitRateWf { get; set; }
        public double? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public double? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public double? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public long? StkId { get; set; }
        public decimal? MrpStrip { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? IsVerifiedDatetime { get; set; }
        public long? IsVerifiedUserId { get; set; }
        public double? DiscPerc2 { get; set; }
        public decimal? DiscAmt2 { get; set; }
    }
}
