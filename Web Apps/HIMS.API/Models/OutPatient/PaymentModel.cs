using System.Drawing.Printing;
using FluentValidation;
using HIMS.API.Models.IPPatient;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Models.OutPatient
{
    public class PaymentsModel
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
        public decimal? TDSAmount { get; set; }
        public decimal? UnitId { get; set; }
        public decimal? Wfamount { get; set; }



    }

    public class PaymentsModelValidator : AbstractValidator<PaymentsModel>
        {
            public PaymentsModelValidator()
            {
                RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            }
        }


        public class BillModel
        {
            public long? BillNo { get; set; }
             public decimal? balanceAmt { get; set; }

        }
        public class BillModelValidator : AbstractValidator<BillModel>
        {
            public BillModelValidator()
            {
                RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            }
        }

        public class AdvanceDetailsModel
        {
            public long? AdvanceDetailID { get; set; }
            public decimal? UsedAmount { get; set; }
            public decimal? BalanceAmount { get; set; }
        }
        public class AdvanceDetailsModelValidator : AbstractValidator<AdvanceDetailsModel>
        {
            public AdvanceDetailsModelValidator()
            {
                RuleFor(x => x.UsedAmount).NotNull().NotEmpty().WithMessage("UsedAmount is required");
                RuleFor(x => x.BalanceAmount).NotNull().NotEmpty().WithMessage("BalanceAmount is required");
            }
        }
        
        public class AdvanceHeadersModel
        {
            public long? AdvanceId { get; set; }
            public decimal? AdvanceUsedAmount { get; set; }
            public decimal? BalanceAmount { get; set; }

        }
        public class AdvanceHeadersModelValidator : AbstractValidator<AdvanceHeadersModel>
        {
            public AdvanceHeadersModelValidator()
            {
                RuleFor(x => x.AdvanceUsedAmount).NotNull().NotEmpty().WithMessage("AdvanceUsedAmount is required");
                RuleFor(x => x.BalanceAmount).NotNull().NotEmpty().WithMessage("BalanceAmount is required");
            }
        }
        public class ModelPayment
        {
            public PaymentsModel Payment { get; set; }
            public BillModel Billupdate { get; set; }
            public List<AdvanceDetailsModel> AdvanceDetailupdate { get; set; }
            public AdvanceHeadersModel AdvanceHeaderupdate { get; set; }


        }
    }
