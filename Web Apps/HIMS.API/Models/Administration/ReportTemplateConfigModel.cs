using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Administration
{
    public class ReportTemplateConfigModel
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDescription { get; set; }
        public long? DepartmentId { get; set; }
        public string? CategoryName { get; set; }
        public string? TemplateHeader { get; set; }
        public string? TemplateFooter { get; set; }
        public bool? IsTemplateWithHeader { get; set; }
        public bool? IsTemplateHeaderWithImage { get; set; }
        public bool? IsTemplateWithFooter { get; set; }
        public bool? IsTemplateFooterWithImage { get; set; }

    }

    public class ReportTemplateConfigModelValidator : AbstractValidator<ReportTemplateConfigModel>
    {
        public ReportTemplateConfigModelValidator()
        {
            RuleFor(x => x.TemplateName).NotNull().NotEmpty().WithMessage("TemplateName is required");
            RuleFor(x => x.TemplateDescription).NotNull().NotEmpty().WithMessage("TemplateDescription is required");
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("CategoryName is required");
            RuleFor(x => x.TemplateHeader).NotNull().NotEmpty().WithMessage("TemplateHeader is required");




        }
    }
}
