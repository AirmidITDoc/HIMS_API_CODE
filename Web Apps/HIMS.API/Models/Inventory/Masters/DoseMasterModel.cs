using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DoseMasterModel
    {
        public long DoseId { get; set; }
        public string? DoseName { get; set; }
        public string? DoseNameInEnglish { get; set; }
        public string? DoseNameInMarathi { get; set; }
        public double? DoseQtyPerDay { get; set; }
    }
    public class DoseMasterModelValidator : AbstractValidator<DoseMasterModel>
    {
        public DoseMasterModelValidator()
        {
            RuleFor(x => x.DoseName).NotNull().NotEmpty().WithMessage("DoseName is required");
            RuleFor(x => x.DoseNameInEnglish).NotNull().NotEmpty().WithMessage("DoseNameInEnglish is required");
            RuleFor(x => x.DoseNameInMarathi).NotNull().NotEmpty().WithMessage("DoseNameInMarathi is required");

        }
    }
}
