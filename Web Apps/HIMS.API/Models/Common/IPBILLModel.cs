﻿using FluentValidation;
using HIMS.API.Models.OutPatient;

namespace HIMS.API.Models.Common
{
    public class IPBILLModel
    {
        public int BillNo { get; set; }
        public int? OPDIPDID { get; set; }
        public float? TotalAmt { get; set; }
        public float? ConcessionAmt { get; set; }
        public float? NetPayableAmt { get; set; }
        public float? PaidAmt { get; set; }
        public float? BalanceAmt { get; set; }
        public string? BillDate { get; set; }
        public int? OPDIPDType { get; set; }
        public int? AddedBy { get; set; }
        public float? TotalAdvanceAmount { get; set; }
        public string? BillTime { get; set; }
        public int? ConcessionReasonId { get; set; }
        public Boolean? IsSettled { get; set; }
        public Boolean? IsPrinted { get; set; }
        public Boolean? IsFree { get; set; }
        public int? CompanyId { get; set; }
        public int? TariffId { get; set; }
        public int? UnitId { get; set; }
        public int? InterimOrFinal { get; set; }
        public int? CompanyRefNo { get; set; }
        public int? ConcessionAuthorizationName { get; set; }
        public float? SpeTaxPer { get; set; }
        public float? SpeTaxAmt { get; set; }
        public int? CompDiscAmt { get; set; }
        public string? DiscComments { get; set; }
        public long? CashCounterId { get; set; }
        public List<IpChargeModel> AddCharges { get; set; }
        public List<IpBillDetailModel> BillDetails { get; set; }
        public List<IPPaymentModel> Payments { get; set; }
    }
    public class IpBillModelValidator : AbstractValidator<IPBILLModel>
    {
        public IpBillModelValidator()
        {
            RuleFor(x => x.OPDIPDID).NotNull().NotEmpty().WithMessage("OPDIPDID is required");
            RuleFor(x => x.TotalAmt).NotNull().NotEmpty().WithMessage("TotalAmt is required");
            RuleFor(x => x.NetPayableAmt).NotNull().NotEmpty().WithMessage("NetPayableAmt is required");
            RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");
            RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
        }
    }
    public class IpBillDetailModel
    {
        public int BillNo { get; set; }
        public int ChargesId { get; set; }
    }

    public class IpBillDetailsModelValidator : AbstractValidator<IpBillDetailModel>
    {
        public IpBillDetailsModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage("ChargesId is required");
        }
    }

    public class IpChargeModel
    {
        public long ChargesId { get; set; }
        public string? ChargesDate { get; set; }
        public int? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public float? Price { get; set; }
        public float? Qty { get; set; }
        public float? TotalAmt { get; set; }
        public float? ConcessionPercentage { get; set; }
        public float? ConcessionAmount { get; set; }
        public float? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public float? DocPercentage { get; set; }
        public float? DocAmt { get; set; }
        public float? HospitalAmt { get; set; }
        public bool? IsGenerated { get; set; }
        public int? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public bool? IsPathology { get; set; }
        public bool? IsRadiology { get; set; }
        public bool? IsPackage { get; set; }
        public int? PackageMainChargeID { get; set; }
        public bool? IsSelfOrCompanyService { get; set; }
        public long? PackageId { get; set; }
        public string? ChargesTime { get; set; }
        public long? ClassId { get; set; }
        public long? BillNo { get; set; }

    }
    public class IpChargesModelValidator : AbstractValidator<IpChargeModel>
    {
        public IpChargesModelValidator()
        {
            //RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
            //RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }

    //public class IPBillPaymentModel
    //{
    //    public int PaymentId { get; set; }
    //    public int? BillNo { get; set; }
    //    public string? ReceiptNo { get; set; }
    //    public string? PaymentDate { get; set; }
    //    public string? PaymentTime { get; set; }
    //    public long? CashPayAmount { get; set; }
    //    public long? ChequePayAmount { get; set; }
    //    public string? ChequeNo { get; set; }
    //    public string? BankName { get; set; }
    //    public string? ChequeDate { get; set; }
    //    public long? CardPayAmount { get; set; }
    //    public string? CardNo { get; set; }
    //    public string? CardBankName { get; set; }
    //    public string? CardDate { get; set; }
    //    public long? AdvanceUsedAmount { get; set; }
    //    public int? AdvanceId { get; set; }
    //    public int? RefundId { get; set; }
    //    public int? TransactionType { get; set; }
    //    public string? Remark { get; set; }
    //    public int? AddBy { get; set; }
    //    public bool IsCancelled { get; set; }
    //    public int? IsCancelledBy { get; set; }
    //    public string? IsCancelledDate { get; set; }
    //    public long? NeftpayAmount { get; set; }
    //    public string? Neftno { get; set; }
    //    public string? NeftbankMaster { get; set; }
    //    public string? Neftdate { get; set; }
    //    public long? PayTmamount { get; set; }
    //    public string? PayTmtranNo { get; set; }
    //    public string? PayTmdate { get; set; }
    //    public long? Tdsamount { get; set; }
    //}

    //public class IPPaymentModelValidator : AbstractValidator<IPBillPaymentModel>
    //{
    //    public IPPaymentModelValidator()
    //    {
    //        RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("ClassId is required");
    //    }
    //}

    //public class InsertPathologyReportHeader
    //{
    //    public DateTime PathDate { get; set; }
    //    public DateTime PathTime { get; set; }
    //    public Boolean OPD_IPD_Type { get; set; }
    //    public int OPD_IPD_Id { get; set; }
    //    public int PathTestID { get; set; }
    //    public int AddedBy { get; set; }
    //    public int ChargeID { get; set; }
    //    public Boolean IsCompleted { get; set; }
    //    public Boolean IsPrinted { get; set; }
    //    public Boolean IsSampleCollection { get; set; }
    //    public Boolean TestType { get; set; }
    //}

    //public class InsertRadiologyReportHeader
    //{
    //    public DateTime RadDate { get; set; }
    //    public DateTime RadTime { get; set; }
    //    public Boolean OPD_IPD_Type { get; set; }
    //    public int OPD_IPD_Id { get; set; }
    //    public int RadTestID { get; set; }
    //    public int AddedBy { get; set; }
    //    public Boolean IsCancelled { get; set; }
    //    public int ChargeID { get; set; }
    //    public Boolean IsPrinted { get; set; }
    //    public Boolean IsCompleted { get; set; }
    //    public Boolean TestType { get; set; }
    //}

    //public class OPoctorShareGroupAdmChargeDoc
    //{
    //    public int BillNo { get; set; }

    //}

    //public class OPCalDiscAmountBill
    //{
    //    public int BillNo { get; set; }

    //}
}
