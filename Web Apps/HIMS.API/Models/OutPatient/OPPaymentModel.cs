using FluentValidation;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.OutPatient
{
    public class OPPaymentdetailModel
    {
        public int PaymentId { get; set; }
        public int? BillNo { get; set; }
        //  public String ReceiptNo { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
        public long? CashPayAmount { get; set; }
        public long? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? ChequeDate { get; set; }
        public long? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public string? CardDate { get; set; }
        public long? AdvanceUsedAmount { get; set; }
        public int? AdvanceId { get; set; }
        public int? RefundId { get; set; }
        public int? TransactionType { get; set; }
        public string? Remark { get; set; }
        public int? AddBy { get; set; }
        public int? IsCancelled { get; set; }
        public int? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public int? OPDIPDType { get; set; }
        public long? NEFTPayAmount { get; set; }
        public string? NEFTNo { get; set; }
        public string? NEFTBankMaster { get; set; }
        public string? NEFTDate { get; set; }
        public long? PayTMAmount { get; set; }
        public string? PayTMTranNo { get; set; }
        public string? PayTMDate { get; set; }
        public float? TDSAmount { get; set; }
    }

    public class PaymentModelValidator : AbstractValidator<OPPaymentModel>
    {
        public PaymentModelValidator()
        {
            //RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            //RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
            //RuleFor(x => x.MobileNo).NotNull().NotEmpty().WithMessage("Mobile is required");
            //RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("Department is required");
            //RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("Doctor is required");
        }
    }
}
