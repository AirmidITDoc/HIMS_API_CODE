using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.OutPatient
{

    public class TDrbillModel
    {
       public long? OPDIPDID { get; set; }
        public decimal? TotalAmt { get; set; }       
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }              
        public decimal? BalanceAmt { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OPDIPDType { get; set; }
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
       public double? TaxPer { get; set; }
       public decimal? TaxAmount { get; set; }
       public long? Drbno { get; set; }
       
    }
    public class TDrbillModelValidator : AbstractValidator<TDrbillModel>
    {
        public TDrbillModelValidator()
        {
            RuleFor(x => x.TotalAmt).NotNull().NotEmpty().WithMessage("TotalAmt is required");
        }
    }

    public class TDRBillDetModel
    {
        public long? DRNo { get; set; }

        public long? ChargesId { get; set; }


    }

    public class TDRBillDetModelValidator : AbstractValidator<TDRBillDetModel>
    {
        public TDRBillDetModelValidator()
        {
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage("ChargesId is required");
        }
    }

    public class TDrbillingModel
    {

        public TDrbillModel TDrbill { get; set; }
       public List<TDRBillDetModel> TDRBillDet { get; set; }

    }



}