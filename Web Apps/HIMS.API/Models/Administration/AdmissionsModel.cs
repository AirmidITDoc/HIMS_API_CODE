using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Administration
{
    public class AdmissionModell
    {
        public long AdmissionID { get; set; }

        public DateTime AdmissionDate { get; set; }

        public string AdmissionTime { get; set; }

    }
    public class AdmissionModellValidator : AbstractValidator<AdmissionModell>
    {
        public AdmissionModellValidator()
        {
               RuleFor(x => x.AdmissionDate).NotNull().NotEmpty().WithMessage("AdmissionDate id is required");
               RuleFor(x => x.AdmissionTime).NotNull().NotEmpty().WithMessage("AdmissionTime  is required");
        }
    }
}