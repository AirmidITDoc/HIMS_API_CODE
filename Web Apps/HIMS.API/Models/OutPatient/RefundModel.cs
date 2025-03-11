using System.Drawing.Printing;
using FluentValidation;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Models.OutPatient
{


    public class RefundModel
    {
       public DateTime? RefundDate { get; set; }
       public string? RefundTime { get; set; }
       public long? BillId { get; set; }
       public long? AdvanceId { get; set; }
       public bool? OPDIPDType { get; set; }
       public long? OPDIPDID { get; set; }
       public decimal? RefundAmount { get; set; }
       public string? Remark { get; set; }
       public long? TransactionId { get; set; }
       public long? AddedBy { get; set; }
       public bool? IsCancelled { get; set; }
       public long? IsCancelledBy { get; set; }
       public DateTime? IsCancelledDate { get; set; }
       public long? RefundId { get; set; }

    }
    public class RefundModelValidator : AbstractValidator<RefundModel>
    {
        public RefundModelValidator()
        {
            RuleFor(x => x.BillId).NotNull().NotEmpty().WithMessage("BillId is required");
        }
    }
    public class AdvanceHeaderMModel
    {
        public int? AdvanceId { get; set; }
        public double? AdvanceUsedAmount { get; set; }
        public double? BalanceAmount { get; set; }

    }
    public class AdvanceHeaderMModelValidator : AbstractValidator<AdvanceHeaderMModel>
    {
        public AdvanceHeaderMModelValidator()
        {
            RuleFor(x => x.AdvanceUsedAmount).NotNull().NotEmpty().WithMessage("AdvanceUsedAmount is required");
            RuleFor(x => x.BalanceAmount).NotNull().NotEmpty().WithMessage("BalanceAmount is required");
        }
    }

    public class AdvanceRefundDetailModel
    {
        public long? AdvDetailId { get; set; }
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public double? AdvRefundAmt { get; set; }

    }
    public class AdvanceRefundDetailModelValidator : AbstractValidator<AdvanceRefundDetailModel>
    {
        public AdvanceRefundDetailModelValidator()
        {
            RuleFor(x => x.AdvRefundAmt).NotNull().NotEmpty().WithMessage("AdvRefundAmt is required");
           
        }
    }
    public class AdvDetailModel
    {
        public long? AdvanceDetailID { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? RefundAmount { get; set; }
    }
    public class AdvDetailModelValidator : AbstractValidator<AdvDetailModel>
    {
        public AdvDetailModelValidator()
        {
            RuleFor(x => x.BalanceAmount).NotNull().NotEmpty().WithMessage("BalanceAmount is required");
            RuleFor(x => x.RefundAmount).NotNull().NotEmpty().WithMessage("RefundAmount is required");
        }
    }
    public class paymentMModel
    {

        public long? BillNo { get; set; }
        public string? ReceiptNo { get; set; }
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
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime? PayTmdate { get; set; }
        public decimal? TDSAmount { get; set; }


    }
    public class paymentMModelValidator : AbstractValidator<paymentMModel>
    {
        public paymentMModelValidator()
        {
            RuleFor(x => x.PaymentDate).NotNull().NotEmpty().WithMessage("PaymentDate is required");
        }
    }
    public class RefundsModel
    {
        public RefundModel Refund { get; set; }
        public AdvanceHeaderMModel advanceHeaderupdate { get; set; }
        public List<AdvanceRefundDetailModel> AdvDetailRefund { get; set; }
        public List<AdvDetailModel> AdveDetailupdate { get; set; }
        public paymentMModel payment { get; set; }


    }


}