using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Administration
{
    public class ReportTemplateConfigModel
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDescription { get; set; }
        public bool IsActive { get; set; }

    }

    public class PathologyTemplateModelValidator : AbstractValidator<PathologyTemplateModel>
    {
        public PathologyTemplateModelValidator()
        {
            //RuleFor(x => x.TemplateName).NotNull().NotEmpty().WithMessage("TemplateName is required");
            //RuleFor(x => x.TemplateDescInHtml).NotNull().NotEmpty().WithMessage("TemplateDescInHtml is required");



        }
    }
}
