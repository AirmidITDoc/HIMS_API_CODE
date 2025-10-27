using FluentValidation;

namespace HIMS.API.Models.OPPatient

{
    public class TCertificateInformationParamModel
    {
        public long? CertificateId { get; set; }
        public DateTime? CertificateDate { get; set; }
        public String? CertificateTime { get; set; }
        public long? VisitId { get; set; }
        public long? CertificateTemplateId { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateText { get; set; }

    }
    public class TCertificateInformationParamModelValidator : AbstractValidator<TCertificateInformationParamModel>

    {
        public TCertificateInformationParamModelValidator()
        {
            RuleFor(x => x.CertificateTemplateId).NotNull().NotEmpty().WithMessage("CertificateTemplateId is required");
            RuleFor(x => x.CertificateDate).NotNull().NotEmpty().WithMessage("CertificateDate is required");
            RuleFor(x => x.CertificateTime).NotNull().NotEmpty().WithMessage("CertificateTime is required");
            RuleFor(x => x.VisitId).NotNull().NotEmpty().WithMessage("VisitId is required");
            RuleFor(x => x.CertificateName).NotNull().NotEmpty().WithMessage("CertificateName is required");
            RuleFor(x => x.CertificateText).NotNull().NotEmpty().WithMessage("CertificateText is required");
        }
    }
}






