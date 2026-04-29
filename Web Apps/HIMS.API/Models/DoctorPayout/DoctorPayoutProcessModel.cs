using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.DoctorPayout
{
    public class DoctorPayoutProcessModel
    {
        public long? DoctorId { get; set; }
        public DateTime? ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
        public DateTime? ProcessDate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? DoctorAmount { get; set; }
        public decimal? HospitalAmount { get; set; }
        public decimal? Tdsamount { get; set; }
        public int? CreatedBy { get; set; }
        public long DoctorPayoutId { get; set; }




    }
    public class DoctorPayoutProcessModelValidator : AbstractValidator<DoctorPayoutProcessModel>
    {
        public DoctorPayoutProcessModelValidator()
        {
            RuleFor(x => x.ProcessStartDate).NotNull().NotEmpty().WithMessage("ProcessStartDate is required");
            RuleFor(x => x.ProcessEndDate).NotNull().NotEmpty().WithMessage(" ProcessEndDate required");
            RuleFor(x => x.ProcessDate).NotNull().NotEmpty().WithMessage("ProcessDate is required");


        }
    }
    public class DoctorPayoutProcessDetailsModel
    {
        public long DoctorPayoutDetId { get; set; }
        public long? DoctorPayoutId { get; set; }
        public long? DoctorId { get; set; }
        public long? ChargeId { get; set; }
        public int? CreatedBy { get; set; }

    }
    public class DoctorPayoutProcessDetailsModelValidator : AbstractValidator<DoctorPayoutProcessDetailsModel>
    {
        public DoctorPayoutProcessDetailsModelValidator()
        {
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId is required");


        }
    }
    public class DoctorPayoutModel
    {
        public DoctorPayoutProcessModel DoctorPayoutProcess {  get; set; }
        public List<DoctorPayoutProcessDetailsModel> DoctorPayoutProcessDetail { get; set; }

    }



    public class DoctorPayyModel
    {
        public long PaymentId { get; set; }
        public long? UnitId { get; set; }
        public long? BillNo { get; set; }
        public byte? Opdipdtype { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
        public decimal? PayAmount { get; set; }
        public string? TranNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ValidationDate { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public string? Comments { get; set; }
        public string? PayMode { get; set; }
        public string? OnlineTranNo { get; set; }
        public string? OnlineTranResponse { get; set; }
        public long? CompanyId { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public long? CashCounterId { get; set; }
        public long? TransactionType { get; set; }
        public string? TransactionLabel { get; set; }
        public byte? IsSelfOrcompany { get; set; }
        public string? TranMode { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }

    }
    public class DoctorPaymwntModel
    {
        public List<DoctorPayyModel> DoctorPayyModel { get; set; }

    }



}
