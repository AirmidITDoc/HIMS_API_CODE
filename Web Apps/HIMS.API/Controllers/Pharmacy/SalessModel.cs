using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class SalessModel
    {

        public long SalesId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public long? ConcessionReasonId { get; set; }
        public long? ConcessionAuthorizationId { get; set; }
        public bool? IsSellted { get; set; }
        public bool? IsPrint { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public string? ExternalPatientName { get; set; }
        public string? DoctorName { get; set; }
        public long? StoreId { get; set; }
        public long? IsPrescription { get; set; }
        public long? AddedBy { get; set; }
        public string? CreditReason { get; set; }
        public long? CreditReasonId { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public float? DiscperH { get; set; }
        public bool? IsPurBill { get; set; }
        public bool? IsBillCheck { get; set; }
        public string? SalesHeadName { get; set; }
        public long? SalesTypeId { get; set; }
        public long? RegId { get; set; }
        public string? ExtMobileNo { get; set; }
        public decimal? RoundOff { get; set; }
        public string? ExtAddress { get; set; }

        public List<SalesDetailModel> TSalesDetails { get; set; }
    }

    public class SalessModelValidator : AbstractValidator<SalessModel>
    {
        public SalessModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
            RuleFor(x => x.OpIpId).NotNull().NotEmpty().WithMessage("OpIpId is required");
        }
    }
    public class CurrentStocksModel
    {
        public long ItemId { get; set; }
        public float IssueQty { get; set; }
        public long IStkId { get; set; }
        public long StoreID { get; set; }
    }

    public class CurrentStocksModelValidator : AbstractValidator<CurrentStocksModel>
    {
        public CurrentStocksModelValidator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId is required");
            RuleFor(x => x.IssueQty).NotNull().NotEmpty().WithMessage("IssueQty is required");
        }
    }
    public class PaymentpharModelSS
    {
        public long? PaymentId { get; set; }
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
        public long? OPDIPDType { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime? PayTmdate { get; set; }
        public decimal? tdsAmount { get; set; }
        public decimal? WfAmount { get; set; }
        public long? UnitId { get; set; }
    }
    public class PaymentpharModelSSValidator : AbstractValidator<PaymentpharModelSS>
    {
        public PaymentpharModelSSValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.CashPayAmount).NotNull().NotEmpty().WithMessage("CashPayAmount is required");
        }
    }

    public class IPPrescriptionsModel
    {
        public long opipid { get; set; }
        public bool Isclosed { get; set; }
      
    }
    public class IPPrescriptionsModelValidator : AbstractValidator<IPPrescriptionsModel>
    {
        public IPPrescriptionsModelValidator()
        {
            RuleFor(x => x.opipid).NotNull().NotEmpty().WithMessage("opipid is required");
          
        }
    }
    public class SalesDraftHeaderModel
    {
        public long DSalesId { get; set; }
        public bool Isclosed { get; set; }

    }
    public class SalesDraftHeaderModelValidator : AbstractValidator<SalesDraftHeaderModel>
    {
        public SalesDraftHeaderModelValidator()
        {
            RuleFor(x => x.DSalesId).NotNull().NotEmpty().WithMessage("DSalesId is required");
          
        }
    }
    public class SaleReqModel
    {
        public SalessModel Sales { get; set; }
        public List<CurrentStocksModel> TCurrentStock { get; set; }
        public PaymentpharModelSS Payment { get; set; }
        public IPPrescriptionsModel Prescription { get; set; }
        public SalesDraftHeaderModel SalesDraft { get; set; }

    }
}
