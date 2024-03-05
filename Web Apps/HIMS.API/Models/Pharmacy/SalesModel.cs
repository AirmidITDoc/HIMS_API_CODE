using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class SalesModel
    {
        public long SalesId { get; set; }
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
        public long? CashCounterId { get; set; }
        public string? ExternalPatientName { get; set; }
        public string? DoctorName { get; set; }
        public long? StoreId { get; set; }
        public long? IsPrescription { get; set; }
        public long? ExtRegNo { get; set; }
        public string? CreditReason { get; set; }
        public long? CreditReasonId { get; set; }
        public decimal? RefundAmt { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public float? DiscperH { get; set; }
        public string? SalesHeadName { get; set; }
        public long? SalesTypeId { get; set; }
        public long? RegId { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? ExtMobileNo { get; set; }
        public decimal? RoundOff { get; set; }

        public List<SalesDetailModel> TSalesDetails { get; set; }
    }
    public class SalesModelValidator : AbstractValidator<SalesModel>
    {
        public SalesModelValidator()
        {
            RuleFor(x => x.SalesHeadName).NotNull().NotEmpty().WithMessage("Prefix Name is required");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Enter valid sex");
        }
    }
}
