using FluentValidation;

namespace HIMS.API.Models.Administration
{
    public class AdmissionsModel
    {
        public long AdmissionID { get; set; }


    }

    public class AdmissionsModelValidator : AbstractValidator<AdmissionsModel>
    {
        public AdmissionsModelValidator()
        {
            RuleFor(x => x.AdmissionID).NotNull().NotEmpty().WithMessage("AdmissionID id is required");
        }
    }
}