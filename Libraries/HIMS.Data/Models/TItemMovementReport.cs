using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TItemMovementReport
    {
        public long MovementId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? TransactionTime { get; set; }
        public string? MovementNo { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public string? TransactionType { get; set; }
        public long? TransactionId { get; set; }
        public string? DocumentNo { get; set; }
        public long? DoumentId { get; set; }
        public long? DocumentDetId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? IssueQty { get; set; }
        public double? ReceiptQty { get; set; }
        public double? BalQty { get; set; }
        public decimal? PerUnitMrp { get; set; }
        public decimal? TotalMrp { get; set; }
        public decimal? PerUnitLandedPrice { get; set; }
        public decimal? TotalLandedAmount { get; set; }
        public decimal? PerUnitPurRate { get; set; }
        public decimal? TotalPurAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public long? SupplierId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public double? ConversionFactor { get; set; }
        public double? VatPer { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? TotalVatAmount { get; set; }
        public double? TaxPer { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalTaxAmount { get; set; }
        public double? DisPercentage { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? TotalDisAmount { get; set; }
        public decimal? ConAmount { get; set; }
        public decimal? TotalConAmount { get; set; }
        public decimal? NetAmount { get; set; }
    }
}
