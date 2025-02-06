using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class PriscriptionclassModel
    {
        public long ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? TemplateDescName { get; set; }

    }
    public class PriscriptionclassModelValidator : AbstractValidator<PriscriptionclassModel>
    {
        public PriscriptionclassModelValidator()
        {
            RuleFor(x => x.ClassName).NotNull().NotEmpty().WithMessage("ClassName  is required");
            RuleFor(x => x.TemplateDescName).NotNull().NotEmpty().WithMessage("TemplateDescName  is required");

        }
    }
}
