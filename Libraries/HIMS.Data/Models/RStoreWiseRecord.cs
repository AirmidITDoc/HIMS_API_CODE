namespace HIMS.Data.Models
{
    public partial class RStoreWiseRecord
    {
        public long StoreRecordId { get; set; }
        public long? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? StoreAddress { get; set; }
        public DateTime? ReportDate { get; set; }
        public long? ReportYear { get; set; }
        public long? ReportQuarter { get; set; }
        public long? ReportMonth { get; set; }
        public long? ReportWeek { get; set; }
        public long? ReportDay { get; set; }
        public long? SalesCount { get; set; }
        public decimal? SalesTotalAmount { get; set; }
        public decimal? SalesNetAmount { get; set; }
        public decimal? SalesPaidAmount { get; set; }
        public decimal? SalesBalanceAmount { get; set; }
        public long? ShCount { get; set; }
        public long? ShOpeningQty { get; set; }
        public long? ShClosingQty { get; set; }
        public long? ShReceivedQty { get; set; }
        public long? ShIssueQty { get; set; }
        public long? ShBalanceQty { get; set; }
        public decimal? ShOpBalByPurchaseRate { get; set; }
        public decimal? ShOpBalByLandedRate { get; set; }
        public decimal? ShOpBalByPurchaseRateWf { get; set; }
        public decimal? ShClosingBalByPurchaseRate { get; set; }
        public decimal? ShClosingBalByLandedRate { get; set; }
        public decimal? ShClosingBalByPurchaseRateWf { get; set; }
        public long? PhCount { get; set; }
        public decimal? PhTotalAmount { get; set; }
        public decimal? PhDiscAmount { get; set; }
        public decimal? PhTaxAmount { get; set; }
        public decimal? PhGrandTotal { get; set; }
        public decimal? PhTotCgstamt { get; set; }
        public decimal? PhTotSgstamt { get; set; }
        public decimal? PhTotIgstamt { get; set; }
        public decimal? PhTransportChanges { get; set; }
        public decimal? PhHandlingCharges { get; set; }
        public decimal? PhFreightCharges { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
