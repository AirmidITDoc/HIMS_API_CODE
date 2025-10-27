using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class PathologyTemplateModel
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDesc { get; set; }
        public string? TemplateDescInHtml { get; set; }
    }

    public class PathologyTemplateModelValidator : AbstractValidator<PathologyTemplateModel>
    {
        public PathologyTemplateModelValidator()
        {
            RuleFor(x => x.TemplateName).NotNull().NotEmpty().WithMessage("TemplateName is required");
            RuleFor(x => x.TemplateDescInHtml).NotNull().NotEmpty().WithMessage("TemplateDescInHtml is required");



        }
    }

}
