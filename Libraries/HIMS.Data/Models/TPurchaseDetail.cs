using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPurchaseDetail
    {
        public long PurDetId { get; set; }
        public long? PurchaseId { get; set; }
        public long? ItemId { get; set; }
        public long? Uomid { get; set; }
        public double? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? VatAmount { get; set; }
        public double? VatPer { get; set; }
        public decimal? GrandTotalAmount { get; set; }
        public decimal? Mrp { get; set; }
        public string? Specification { get; set; }
        public bool? IsClosed { get; set; }
        public double? PobalQty { get; set; }
        public double? IsGrnQty { get; set; }
        public double? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public double? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public double? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public decimal? DefRate { get; set; }
        public float? VendDiscPer { get; set; }
        public decimal? VendDiscAmt { get; set; }

        public virtual TPurchaseHeader? Purchase { get; set; }
    }
}
