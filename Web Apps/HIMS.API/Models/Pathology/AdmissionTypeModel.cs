using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Pathology
{
    public class AdmissionTypeModel
    {
        public long AdmissiontypeId { get; set; }
        public string? AdmissiontypeName { get; set; }
    }
    public class AdmissionTypeModelValidator : AbstractValidator<AdmissionTypeModel>
    {
        public AdmissionTypeModelValidator()
        {
            RuleFor(x => x.AdmissiontypeName).NotNull().NotEmpty().WithMessage("AdmissiontypeName is required");

        }
    }

}
