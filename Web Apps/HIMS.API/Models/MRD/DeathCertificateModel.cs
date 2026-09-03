using System;
using FluentValidation;

namespace HIMS.API.Models.MRD
{
    public class DeathCertificateModel
    {
        public long CertificateId { get; set; }
        public DateTime? CertificateDate { get; set; }
        public DateTime? CertificateTime { get; set; }
        public long? OpIpId { get; set; }
        public bool? OpIpType { get; set; }
        public DateTime? DateofDeath { get; set; }
        public DateTime? TimeOfDeath { get; set; }
        public string? CauseofDeath { get; set; }
        public string? PlaceOfDeath { get; set; }
        public string? ResponsiblePersonName { get; set; }
        public string? Smcno { get; set; }
        public string? Diagnsis { get; set; }
    }

    //public class DeathCertificateModelValidator : AbstractValidator<DeathCertificateModel>
    //{
    //    public DeathCertificateModelValidator()
    //    {
    //        RuleFor(x => x.CertificateNo).NotNull().NotEmpty().WithMessage("Certificate No is required");
    //    }
    //}
}