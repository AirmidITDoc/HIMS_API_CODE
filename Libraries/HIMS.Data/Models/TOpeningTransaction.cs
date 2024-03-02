using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOpeningTransaction
    {
        public long OpeningId { get; set; }
        public long? OpeningDocNo { get; set; }
        public string? StoreId { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? OpeningTime { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? PerUnitPurRate { get; set; }
        public decimal? PerUnitMrp { get; set; }
        public double? VatPer { get; set; }
        public float? BalQty { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
