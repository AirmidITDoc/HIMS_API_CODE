using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class CertificateTemplateModel
    {
        public long TemplateId { get; set; }
        public string? TemplateDesc { get; set; }
    }
    public class CertificateTemplateModelValidator : CommonDynamicValidator<CertificateTemplateModel>
    {
        public CertificateTemplateModelValidator()
        {
            RuleFor(x => x.TemplateDesc).NotNull().NotEmpty().WithMessage("TemplateDesc is required");
        }
    }
}
