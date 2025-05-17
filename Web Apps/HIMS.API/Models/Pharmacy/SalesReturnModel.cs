using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OutPatient;

namespace HIMS.API.Models.Pharmacy
{
    public class SalesReturnModel
    {
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
        public long SalesReturnId { get; set; }
      //  public List<SalesReturnDetailsModel> TSalesReturnDetails { get; set; }
      //  public List<CurrentStockModels> TCurrentStock { get; set; }

    //    public List<SalesDetailsModel> TSalesDetails { get; set; }



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
        //public decimal? Mrp { get; set; }
        //public decimal? Mrptotal { get; set; }

    }
    public class CurrentStockModels
    {
        public long? ItemId { get; set; }
        public float? IssueQty { get; set; }
        public long? StoreId { get; set; }
        public long? IstkId { get; set; }


    }

    public class SalesDetailsModel
    {
        public long? SalesDetId { get; set; }
        public float? ReturnQty { get; set; }
     
    }
    public class TSalesReturnModel
    {
        public long? SalesReturnId { get; set; }
       

    }
    public class TSalesReturnsModel
    {
        public long? SalesReturnId { get; set; }


    }
    public class SalesHeaderModel
    {
        public long? Id { get; set; }
        public long? TypeId { get; set; }
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

    }
    public class SalesReturnsModel
    {

        public SalesReturnModel? SalesReturn { get; set; }
        public List<SalesReturnDetailsModel?> SalesReturnDetails { get; set; }
        public List<CurrentStockModels>? CurrentStock { get; set; }
        public List<SalesDetailsModel>? SalesDetail { get; set; }
        public TSalesReturnModel? TSalesReturn { get; set; }
        public TSalesReturnsModel? TSalesReturns { get; set; }
        public SalesHeaderModel? SalesHeader { get; set; }
       public PaymentModels? Payment { get; set; }
    }
}
