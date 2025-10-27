namespace HIMS.Data.DTO.Purchase
{
    public class SupplierPaymentStatusListDto
    {
        public long SupplierId { get; set; }
        public long? GRNID { get; set; }
        public string? SupplierName { get; set; }
        public long? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? GrnNumber { get; set; }
        public string? GRNDate { get; set; }
        public string? GRNTime { get; set; }
        public string? InvoiceNo { get; set; }
        public string? DeliveryNo { get; set; }
        public string? GateEntryNo { get; set; }
        public bool? Cash_CreditType { get; set; }
        public bool? GRNType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalDiscAmount { get; set; }
        public decimal? TotalVATAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalAmount { get; set; }
        public string? Remark { get; set; }
        public string? ReceivedBy { get; set; }
        public bool IsVerified { get; set; }
        public long IsClosed { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsPaymentProcess { get; set; }
        public string? PaymentPrcDate { get; set; }
        public string? ProcessDes { get; set; }
        public string? InvDate { get; set; }
        public string? DaysOfInv { get; set; }
        public decimal? ReturnNetAmt { get; set; }

    }
}
