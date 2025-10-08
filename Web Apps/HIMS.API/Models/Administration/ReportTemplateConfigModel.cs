using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Administration
{
    public class ReportTemplateConfigModel
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDescription { get; set; }
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsTemplateWithHeader { get; set; }
        public bool? IsTemplateHeaderWithImage { get; set; }

    }

    public class ReportTemplateConfigModelValidator : AbstractValidator<ReportTemplateConfigModel>
    {
        public ReportTemplateConfigModelValidator()
        {
            RuleFor(x => x.TemplateName).NotNull().NotEmpty().WithMessage("Template Name is required");
            RuleFor(x => x.TemplateDescription).NotNull().NotEmpty().WithMessage("Template Description is required");
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("CategoryName is required");
        }
    }
}
