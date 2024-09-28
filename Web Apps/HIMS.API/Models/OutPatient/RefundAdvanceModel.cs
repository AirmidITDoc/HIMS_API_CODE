using FluentValidation;
using HIMS.API.Models.IPPatient;
namespace HIMS.API.Models.OutPatient
{
    public class RefundAdvanceModel
    {
        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        //public string? RefundNo { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public bool? Opdipdtype { get; set; }
        public long? Opdipdid { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public long? TransactionId { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        //public long? CashCounterId { get; set; }
        //public bool? IsRefundFlag { get; set; }
        //public int? CreatedBy { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }

    public class RefundAdvanceModelValidator : AbstractValidator<RefundAdvanceModel>
    {
        public RefundAdvanceModelValidator()
        {
            RuleFor(x => x.RefundDate).NotNull().NotEmpty().WithMessage("RefundDate is required");

        }
        public class AdvanceHeaderModel
        {
            public long AdvanceId { get; set; }
            //public DateTime? Date { get; set; }
            //public long? RefId { get; set; }
            //public byte? OpdIpdType { get; set; }
            //public long? OpdIpdId { get; set; }
            //public double? AdvanceAmount { get; set; }
            public double? AdvanceUsedAmount { get; set; }
            public double? BalanceAmount { get; set; }
            //public long? AddedBy { get; set; }
            //public bool? IsCancelled { get; set; }
            //public long? IsCancelledBy { get; set; }
            //public DateTime? IsCancelledDate { get; set; }
        }
        public class AdvanceHeaderModelValidator : AbstractValidator<AdvanceHeaderModel>
        {
            public AdvanceHeaderModelValidator()
            {
                RuleFor(x => x.AdvanceUsedAmount).NotNull().NotEmpty().WithMessage("AdvanceUsedAmount is required");

            }

        }

        public  class AdvRefundDetailModel
        {
            public long AdvRefId { get; set; }
            public double? AdvDetailId { get; set; }
            public DateTime? RefundDate { get; set; }
            public DateTime? RefundTime { get; set; }
            public double? AdvRefundAmt { get; set; }
        }

        public class AdvRefundDetailModelValidator : AbstractValidator<AdvRefundDetailModel>
        {
            public AdvRefundDetailModelValidator()
            {
                RuleFor(x => x.RefundDate).NotNull().NotEmpty().WithMessage("RefundDate is required");

            }

        }

        public partial class AdvanceDetailModel1
        {
            public long AdvanceDetailId { get; set; }
            //public DateTime? Date { get; set; }
            //public DateTime? Time { get; set; }
            //public long? AdvanceId { get; set; }
            //public string? AdvanceNo { get; set; }
            //public long? RefId { get; set; }
            //public long? TransactionId { get; set; }
            //public long? OpdIpdId { get; set; }
            //public byte? OpdIpdType { get; set; }
            //public decimal? AdvanceAmount { get; set; }
            //public decimal? UsedAmount { get; set; }
            public decimal? BalanceAmount { get; set; }
            public decimal? RefundAmount { get; set; }
            //public long? ReasonOfAdvanceId { get; set; }
            //public long? AddedBy { get; set; }
            //public bool? IsCancelled { get; set; }
            //public long? IsCancelledby { get; set; }
            //public DateTime? IsCancelledDate { get; set; }
            //public string? Reason { get; set; }

        }

        public class AdvanceDetailModelValidator : AbstractValidator<AdvanceDetailModel>
        {
            public AdvanceDetailModelValidator()
            {
                RuleFor(x => x.RefundAmount).NotNull().NotEmpty().WithMessage("RefundAmount is required");

            }

        }
        public partial class PaymentModel2
        {
            public long PaymentId { get; set; }
            public long? BillNo { get; set; }
            public string? ReceiptNo { get; set; }
            public DateTime? PaymentDate { get; set; }
            public DateTime? PaymentTime { get; set; }
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
            //public long? CashCounterId { get; set; }
            //public byte? IsSelfOrcompany { get; set; }
            //public long? CompanyId { get; set; }
            public decimal? NeftpayAmount { get; set; }
            public string? Neftno { get; set; }
            public string? NeftbankMaster { get; set; }
            public DateTime? Neftdate { get; set; }
            public decimal? PayTmamount { get; set; }
            public string? PayTmtranNo { get; set; }
            public DateTime? PayTmdate { get; set; }
            //public decimal? ChCashPayAmount { get; set; }
            //public decimal? ChChequePayAmount { get; set; }
            //public decimal? ChCardPayAmount { get; set; }
            //public decimal? ChAdvanceUsedAmount { get; set; }
            //public decimal? ChNeftpayAmount { get; set; }
            //public decimal? ChPayTmamount { get; set; }
            //public string? TranMode { get; set; }
            //public decimal? Tdsamount { get; set; }
        }
        public class PaymentModelValidator : AbstractValidator<PaymentModel2>
        {
            public PaymentModelValidator()
            {
                RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");

            }

        }
        public class RefundAdvance
        {

            public RefundAdvanceModel RefundAdvanceModel { get; set; }
            public AdvanceHeaderModel AdvanceHeaderModel { get; set; }
            public AdvRefundDetailModel AdvRefundDetailModel { get; set; }
            public AdvanceDetailModel1 AdvanceDetailModel1 { get; set; }
            public PaymentModel2 PaymentModel2 { get; set; }
        }
    }
    }
