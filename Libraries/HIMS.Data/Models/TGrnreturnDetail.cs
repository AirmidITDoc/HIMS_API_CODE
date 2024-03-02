using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TGrnreturnDetail
    {
        public long GrnreturnDetailId { get; set; }
        public long? GrnreturnId { get; set; }
        public long? Grnid { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpiryDate { get; set; }
        public float? ReturnQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? UnitPurchaseRate { get; set; }
        public float? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? OtherTaxAmount { get; set; }
        public float? OctroiPer { get; set; }
        public decimal? OctroiAmt { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public decimal? PurchaseTotalAmount { get; set; }
        public short? Conversion { get; set; }
        public string? Remarks { get; set; }
        public long? StkId { get; set; }
        public float? Cf { get; set; }
        public float? TotalQty { get; set; }
    }
}
