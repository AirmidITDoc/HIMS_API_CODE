using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.MRD
{
    public class MRDTemplateModel
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDesc { get; set; }
        public bool? IsActive { get; set; }
    }

    public class MRDTemplateModelValidator : AbstractValidator<MRDTemplateModel>
    {
        public MRDTemplateModelValidator()
        {
            RuleFor(x => x.TemplateName).NotNull().NotEmpty().WithMessage("TemplateName is required");
        }
    }
}