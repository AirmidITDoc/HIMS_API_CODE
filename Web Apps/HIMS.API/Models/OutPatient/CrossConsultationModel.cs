using FluentValidation;


namespace HIMS.API.Models.OutPatient
{
    public class CrossConsultationModel
    {
        public long VisitID { get; set; }
        public long? RegId { get; set; }
        public string? VisitDate { get; set; }
        public string? VisitTime { get; set; }
        public long? UnitId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? ConsultantDocId { get; set; }
        public long? RefDocId { get; set; }
        //public string? Opdno { get; set; }
        public long? TariffId { get; set; }
        public long? CompanyId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }

        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public long? ClassId { get; set; }
        public long? DepartmentId { get; set; }
        public long? PatientOldNew { get; set; }
        public int? FirstFollowupVisit { get; set; }
        public long? AppPurposeId { get; set; }
        public string? FollowupDate { get; set; }
        //public bool? IsMark { get; set; }
        //public string? Comments { get; set; }
        //public bool? IsXray { get; set; }

        public int? CrossConsulFlag { get; set; }
        public long? PhoneAppId { get; set; }
    }
    public class CrossConsultationModelValidator : AbstractValidator<CrossConsultationModel>
    {
        public CrossConsultationModelValidator()
        {
            RuleFor(x => x.RegId).NotNull().NotEmpty().WithMessage("RegId is required");
            RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");
            RuleFor(x => x.PatientTypeId).NotNull().NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.ConsultantDocId).NotNull().NotEmpty().WithMessage("ConsultantDocId is required");
            RuleFor(x => x.RefDocId).NotNull().NotEmpty().WithMessage("RefDocId is required");
            RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
            RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is required");
        }
    }
}
