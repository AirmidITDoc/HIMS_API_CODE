using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class PharaModel
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
        }
        public class PharaModelValidator : AbstractValidator<PharaModel>
        {
            public PharaModelValidator()
            {
                RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
                RuleFor(x => x.CashPayAmount).NotNull().NotEmpty().WithMessage("CashPayAmount is required");
            }
        }
    public class SaleModel
    {
        public long? SalesID { get; set; }
        public double? BalanceAmount { get; set; }
        public double? RefundAmt { get; set; }
    }
    public class SaleModelValidator : AbstractValidator<SaleModel>
    {
        public SaleModelValidator()
        {
            RuleFor(x => x.BalanceAmount).NotNull().NotEmpty().WithMessage("BalanceAmount is required");
            RuleFor(x => x.RefundAmt).NotNull().NotEmpty().WithMessage("RefundAmt is required");
        }
    }

    public class AdvanceDetailModel3
    {
        public long? AdvanceDetailID { get; set; }
        public double? UsedAmount { get; set; }
        public double? BalanceAmount { get; set; }
    }
    public class AdvanceDetailModel3Validator : AbstractValidator<AdvanceDetailModel3>
    {
        public AdvanceDetailModel3Validator()
        {
            RuleFor(x => x.UsedAmount).NotNull().NotEmpty().WithMessage("UsedAmount is required");
            RuleFor(x => x.BalanceAmount).NotNull().NotEmpty().WithMessage("BalanceAmount is required");
        }
    }
    public class TPHAdvanceHeaderModel
    {
        public long? AdvanceId { get; set; }
        public double? AdvanceUsedAmount { get; set; }
        public double? BalanceAmount { get; set; }
    }
    public class TPHAdvanceHeaderModelValidator : AbstractValidator<TPHAdvanceHeaderModel>
    {
        public TPHAdvanceHeaderModelValidator()
        {
            RuleFor(x => x.AdvanceUsedAmount).NotNull().NotEmpty().WithMessage("AdvanceUsedAmount is required");
            RuleFor(x => x.BalanceAmount).NotNull().NotEmpty().WithMessage("BalanceAmount is required");
        }
    }
    public class PharmacyModel
    {
        public List<PharaModel> Payment { get; set; }
        public List<SaleModel> Saless { get; set; }
        public List<AdvanceDetailModel3> AdvanceDetail { get; set; }
        public TPHAdvanceHeaderModel AdvanceHeader { get; set; }
        //public SalesDraftHeaderModel SalesDraft { get; set; }

    }

}
