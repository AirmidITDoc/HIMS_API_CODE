using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class RadiologyTemplateModel
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDesc { get; set; }
    }
    public class RadiologyTemplateModelValidator : AbstractValidator<RadiologyTemplateModel>
    {
        public RadiologyTemplateModelValidator()
        {
            RuleFor(x => x.TemplateName).NotNull().NotEmpty().WithMessage("TemplateName is required");
            RuleFor(x => x.TemplateDesc).NotNull().NotEmpty().WithMessage("TemplateDesc is required");

        }
    }

}
