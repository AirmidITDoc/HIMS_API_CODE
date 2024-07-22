using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CertificateTemplateModel
    {
        public long TemplateId { get; set; }
        public string? TemplateDesc { get; set; }
    }
    public class CertificateTemplateModelValidator : AbstractValidator<CertificateTemplateModel>
    {
        public CertificateTemplateModelValidator()
        {
            RuleFor(x => x.TemplateDesc).NotNull().NotEmpty().WithMessage("Certificate is required");
        }
    }
}
