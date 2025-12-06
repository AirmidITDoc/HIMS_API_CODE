using FluentValidation;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.OutPatient
{

    public class AddChargeuModel
    {
        public long? ChargesId { get; set; }


    }
    public class AddChargeuModelValidator : AbstractValidator<AddChargeuModel>
    {
        public AddChargeuModelValidator()
        {
            //       RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }

    public class IPBilllingModel
    {
        public long? BillNo { get; set; }
        public long? OpdIpdId { get; set; }
        public long? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? Ipdno { get; set; }
        public long? AgeYear { get; set; }
        public long? AgeMonth { get; set; }
        public long? AgeDays { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public bool? PatientType { get; set; }
        public string? CompanyName { get; set; }
        public decimal? CompanyAmt { get; set; }
        public decimal? PatientAmt { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? AddedBy { get; set; }
        public decimal? TotalAdvanceAmount { get; set; }
        public string? BillTime { get; set; }
        public long? ConcessionReasonId { get; set; }
        public bool? IsSettled { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsFree { get; set; }
        public long? CompanyId { get; set; }
        public long? TariffId { get; set; }
        public long? UnitId { get; set; }
        public long? InterimOrFinal { get; set; }
        public string? CompanyRefNo { get; set; }
        public long? ConcessionAuthorizationName { get; set; }
        public double? SpeTaxPer { get; set; }
        public decimal? SpeTaxAmt { get; set; }
        public string? DiscComments { get; set; }
        public decimal? CompDiscAmt { get; set; }
        public long? CashCounterId { get; set; }
        public long? CreatedBy { get; set; }
    }
    public class IPBilllingModelValidator : AbstractValidator<IPBilllingModel>
    {
        public IPBilllingModelValidator()
        {
            RuleFor(x => x.TotalAmt).NotNull().NotEmpty().WithMessage("TotalAmt is required");
        }
    }

    public class BillingDetailsModel
    {
        public long? BillNo { get; set; }

        public long? ChargesId { get; set; }

    }
    public class BillingDetailsModelValidator : AbstractValidator<BillingDetailsModel>
    {
        public BillingDetailsModelValidator()
        {
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage("ChargesId is required");
        }
    }



    public class paymentsModel
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
        public double? Tdsamount { get; set; }
        public decimal? Wfamount { get; set; }
        public long? UnitId { get; set; }


    }
    public class paymentsModelValidator : AbstractValidator<paymentsModel>
    {
        public paymentsModelValidator()
        {
            RuleFor(x => x.PaymentDate).NotNull().NotEmpty().WithMessage("PaymentDate is required");
        }
    }



    public class IPBillModel
    {

        public AddChargeuModel? AddChargeM { get; set; }
        public IPBilllingModel? IPBillling { get; set; }
        public List<BillingDetailsModel>? BillingDetails { get; set; }
        public paymentsModel? payments { get; set; }
        public List<TPaymentModel> TPayments { get; set; }

        //public paymentModel? payment { get; set; }
        //public BillMModel? Bills { get; set; }
        //public List<AdvancesDetailModel?> Advancesupdate { get; set; }
        //public AdvancesHeaderModel? advancesHeaderupdate { get; set; }
        //public AddChargessModel? AddChargessupdate { get; set; }
    }
}