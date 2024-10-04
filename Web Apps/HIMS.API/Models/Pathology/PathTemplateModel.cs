using FluentValidation;

namespace HIMS.API.Models.Pathology
{
    public class PathTemplateModel
    {
        
            public long TemplateId { get; set; }
            public string? TemplateName { get; set; }
            public string? TemplateDesc { get; set; }
            public string? TemplateDescInHtml { get; set; }
    }

     public class PathTemplateModelValidator : AbstractValidator<PathTemplateModel>
     {
            public PathTemplateModelValidator()
            {
                RuleFor(x => x.TemplateName).NotNull().NotEmpty().WithMessage("TemplateName is required");
                RuleFor(x => x.TemplateDesc).NotNull().NotEmpty().WithMessage("TemplateDesc is required");
                RuleFor(x => x.TemplateDescInHtml).NotNull().NotEmpty().WithMessage("TemplateDescInHtml is required");

            }
     }
}

