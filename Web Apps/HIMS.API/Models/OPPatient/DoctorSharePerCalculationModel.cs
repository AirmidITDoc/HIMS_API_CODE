using FluentValidation;

namespace HIMS.API.Models.OPPatient
{
    public class DoctorSharePerCalculationModel
    {

        public long BillNo { get; set; }
        public DateTime? BillDate { get; set; }

    }
    public class DoctorSharePerCalculationModelValidator : AbstractValidator<DoctorSharePerCalculationModel>
    {
        public DoctorSharePerCalculationModelValidator()
        {
            RuleFor(x => x.BillDate).NotNull().NotEmpty().WithMessage("BillDate is required");

        }
    }
}
