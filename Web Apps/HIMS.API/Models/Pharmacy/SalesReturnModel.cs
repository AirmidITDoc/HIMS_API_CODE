using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class SalesReturnModel
    {
        public long SalesReturnId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? SalesId { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public bool? IsSellted { get; set; }
        public bool? IsPrint { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public long? AddedBy { get; set; }
        public long? StoreId { get; set; }
        public string? Narration { get; set; }
        public bool? IsPurBill { get; set; }




    }
    public class SalesReturnModelValidator : AbstractValidator<SalesReturnModel>
    {
        public SalesReturnModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date  is required");
            RuleFor(x => x.Time).NotNull().NotEmpty().WithMessage("Time Time is required");

        }
    }
    public class SalesReturnDetailsModel
    {
        public long SalesReturnID { get; set; }
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

    }
    public class SalesReturnDetailsModelValidator : AbstractValidator<SalesReturnDetailsModel>
    {
        public SalesReturnDetailsModelValidator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
            RuleFor(x => x.Qty).NotNull().NotEmpty().WithMessage("Qty Time is required");

        }
    }
    public class CurrentStockModels
    {
        public long? ItemId { get; set; }
        public float? IssueQty { get; set; }
        public long? StoreId { get; set; }
        public long? IstkId { get; set; }

    }

    public class CurrentStockModelsValidator : AbstractValidator<CurrentStockModels>
    {
        public CurrentStockModelsValidator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
            RuleFor(x => x.IssueQty).NotNull().NotEmpty().WithMessage("IssueQty Time is required");

        }
    }

    public class SalesDetailsModel
    {
        public long? SalesDetId { get; set; }
        public double? ReturnQty { get; set; }

    }

    public class SalesDetailsModelValidator : AbstractValidator<SalesDetailsModel>
    {
        public SalesDetailsModelValidator()
        {
            RuleFor(x => x.SalesDetId).NotNull().NotEmpty().WithMessage("SalesDetId  is required");
            RuleFor(x => x.ReturnQty).NotNull().NotEmpty().WithMessage("ReturnQty Time is required");

        }
    }
    public class SalesHeaderModel
    {
        public long? Id { get; set; }
        public long? TypeId { get; set; }
    }
    public class SalesHeaderModelValidator : AbstractValidator<SalesHeaderModel>
    {
        public SalesHeaderModelValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id  is required");


        }
    }
    public class PaymentModels
    {
        public long PaymentId { get; set; }
        public long? BillNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public DateTime? CardDate { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public long? TransactionType { get; set; }
        public string? Remark { get; set; }
        public long? AddBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public int? OPDIPDType { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime? PayTmdate { get; set; }
        public decimal? TdsAmount { get; set; }
        public decimal? WFAmount { get; set; }
        public long? UnitId { get; set; }

    }

    public class PaymentModelsValidator : AbstractValidator<PaymentModels>
    {
        public PaymentModelsValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo  is required");
            RuleFor(x => x.CashPayAmount).NotNull().NotEmpty().WithMessage("CashPayAmount  is required");
            RuleFor(x => x.BankName).NotNull().NotEmpty().WithMessage("BankName  is required");


        }
    }
    public class SalesReturnsModel
    {
        public SalesReturnModel SalesReturn { get; set; }
        public List<SalesReturnDetailsModel> SalesReturnDetails { get; set; }
        public List<CurrentStockModels> CurrentStock { get; set; }
        public List<SalesDetailsModel> SalesDetail { get; set; }
        public PaymentModels? Payment { get; set; }
        public List<TPaymentpharModelS> TPayments { get; set; }

    }
}
