using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.MRD
{
    public class MedicolegalCertificateModel
    {
        public long DocId { get; set; }
        public DateTime? Mlcdate { get; set; }
        public string? Mlctime { get; set; }
        public string? CertificateNo { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public DateTime? AccidentDate { get; set; }
        public string? AccidentTime { get; set; }
        public string? DetailsInjuries { get; set; }
        public string? AgeofInjuries { get; set; }
        public string? CauseofInjuries { get; set; }
        public long? TreatingDoctorId { get; set; }
        public long? TreatingDoctorId1 { get; set; }
        public long? TreatingDoctorId2 { get; set; }
    }
    public class MedicolegalCertificateModelValidator : AbstractValidator<MedicolegalCertificateModel>
    {
        public MedicolegalCertificateModelValidator()
        {
            RuleFor(x => x.Mlcdate).NotNull().NotEmpty().WithMessage("Mlcdate  is required");
            RuleFor(x => x.Mlctime).NotNull().NotEmpty().WithMessage("Mlctime  is required");
            RuleFor(x => x.CertificateNo).NotNull().NotEmpty().WithMessage("CertificateNo  is required");
            RuleFor(x => x.OpIpId).NotNull().NotEmpty().WithMessage("OpIpId  is required");
            RuleFor(x => x.AccidentDate).NotNull().NotEmpty().WithMessage("AccidentDate  is required");
            RuleFor(x => x.AccidentTime).NotNull().NotEmpty().WithMessage("AccidentTime  is required");
            RuleFor(x => x.DetailsInjuries).NotNull().NotEmpty().WithMessage("DetailsInjuries  is required");
            RuleFor(x => x.CauseofInjuries).NotNull().NotEmpty().WithMessage("CauseofInjuries  is required");
            RuleFor(x => x.TreatingDoctorId).NotNull().NotEmpty().WithMessage("TreatingDoctorId  is required");
            RuleFor(x => x.TreatingDoctorId1).NotNull().NotEmpty().WithMessage("TreatingDoctorId1  is required");
            RuleFor(x => x.TreatingDoctorId2).NotNull().NotEmpty().WithMessage("TreatingDoctorId2  is required");

        }
    }
}
