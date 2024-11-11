using FluentValidation;


namespace HIMS.API.Models.OPPatient
{
    public class ConsRefDoctorModel
    {
        public long VisitId { get; set; }
        public long? RegId { get; set; }
        //public DateTime? VisitDate { get; set; }
        //public DateTime? VisitTime { get; set; }
        //public long? UnitId { get; set; }
        //public long? PatientTypeId { get; set; }
        public long? ConsultantDocId { get; set; }
        public long? RefDocId { get; set; }
        //public string? Opdno { get; set; }
        //public long? TariffId { get; set; }
        //public long? CompanyId { get; set; }
        //public long? AddedBy { get; set; }
        //public long? UpdatedBy { get; set; }
        //public long? IsCancelledBy { get; set; }
        //public bool? IsCancelled { get; set; }
        //public DateTime? IsCancelledDate { get; set; }
        //public long? ClassId { get; set; }
        public long? DepartmentId { get; set; }
        //public long? PatientOldNew { get; set; }
        //public long? FirstFollowupVisit { get; set; }
        //public long? AppPurposeId { get; set; }
        //public DateTime? FollowupDate { get; set; }
        //public bool? IsMark { get; set; }
        //public string? Comments { get; set; }
        //public bool? IsXray { get; set; }
        //public int? CrossConsulFlag { get; set; }
        //public long? PhoneAppId { get; set; }
    }
    public class ConsRefDoctorModelValidator : AbstractValidator<ConsRefDoctorModel>
    {
        public ConsRefDoctorModelValidator()
        {
            RuleFor(x => x.RegId).NotNull().NotEmpty().WithMessage("regId is required");
            RuleFor(x => x.ConsultantDocId).NotNull().NotEmpty().WithMessage("ConsultantDocId is required");
            RuleFor(x => x.RefDocId).NotNull().NotEmpty().WithMessage("RefDocId is required");
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId is required");
           
        }
    }
}
