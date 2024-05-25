using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class GRNModel
    {
        public long Grnid { get; set; }
        public string? GrnNumber { get; set; }
        public DateTime? Grndate { get; set; }
        public DateTime? Grntime { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public string? InvoiceNo { get; set; }
        public string? DeliveryNo { get; set; }
        public string? GateEntryNo { get; set; }
        public bool? CashCreditType { get; set; }
        public bool? Grntype { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalDiscAmount { get; set; }
        public decimal? TotalVatamount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Remark { get; set; }
        public string? ReceivedBy { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsClosed { get; set; }
        public DateTime?  InvDate { get; set; }
        public decimal? DebitNote { get; set; }
        public decimal? CreditNote { get; set; }
        public decimal? OtherCharge { get; set; }
        public decimal? RoundingAmt { get; set; }
        public decimal? TotCgstamt { get; set; }
        public decimal? TotSgstamt { get; set; }
        public decimal? TotIgstamt { get; set; }
        public long? TranProcessId { get; set; }
        public string? TranProcessMode { get; set; }
        public string? EwayBillNo { get; set; }
        public DateTime? EwayBillDate { get; set; }
        public decimal? BillDiscAmt { get; set; }
        public List<GRNDetailModel> TGrndetails { get; set; }
    }
    public class GRNModelValidator : AbstractValidator<GRNModel>
    {
        public GRNModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("Store is required");
            RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("Supplier is required");
            RuleFor(x => x.InvoiceNo).NotNull().NotEmpty().WithMessage("Invoice No is required");
        }
    }

    public class GRNReqDto
    {
        public GRNModel Grn { get; set; }
        public List<ItemModel> GrnItems { get; set; }
        public List<POAganistGRNModel> GrnPODetails { get; set; }
        public List<POHeaderAganistGRNModel> GrnPOHeaders { get; set; }
    }
}
