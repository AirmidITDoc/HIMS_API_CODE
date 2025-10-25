using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.DoctorPayout
{
    public class DoctorPayoutProcessModel
    {
        public long DoctorPayoutId { get; set; }
        public long? DoctorId { get; set; }
        public DateTime? ProcessStartDate { get; set; }
        public DateTime? ProcessEndDate { get; set; }
        public DateTime? ProcessDate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? DoctorAmount { get; set; }
        public decimal? HospitalAmount { get; set; }
        public decimal? Tdsamount { get; set; }
        public List<DoctorPayoutProcessDetailsModel> TDoctorPayoutProcessDetails { get; set; }

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
    }
    public class DoctorPayoutProcessDetailsModelValidator : AbstractValidator<DoctorPayoutProcessDetailsModel>
    {
        public DoctorPayoutProcessDetailsModelValidator()
        {
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId is required");
           

        }
    }


}
