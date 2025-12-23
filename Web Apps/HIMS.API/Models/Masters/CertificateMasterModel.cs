using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CertificateMasterModel
    {
        public long CertificateId { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateDesc { get; set; }

    }
    public class CertificateMasterModelValidator : AbstractValidator<CertificateMasterModel>
    {
        public CertificateMasterModelValidator()
        {
            RuleFor(x => x.CertificateName).NotNull().NotEmpty().WithMessage("CertificateName is required");
            RuleFor(x => x.CertificateDesc).NotNull().NotEmpty().WithMessage("CertificateDesc is required");

        }
    }
}

