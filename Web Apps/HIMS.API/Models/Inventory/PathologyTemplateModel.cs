using FluentValidation;
using HIMS.API.Models.OutPatient;

namespace HIMS.API.Models.Inventory
{
    public class PathologyTemplateModel
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDesc { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? TemplateDescInHtml { get; set; }
    }

    public class PathologyTemplateModelValidator : AbstractValidator<PathologyTemplateModel>
    {
        public PathologyTemplateModelValidator()
        {
            RuleFor(x => x.TemplateName).NotNull().NotEmpty().WithMessage("TemplateName is required");
            RuleFor(x => x.TemplateDesc).NotNull().NotEmpty().WithMessage("TemplateDesc is required");
            
        }
    }
   
}
