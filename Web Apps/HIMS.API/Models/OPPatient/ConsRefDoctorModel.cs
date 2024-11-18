using FluentValidation;


namespace HIMS.API.Models.OPPatient
{
    public class ConsRefDoctorModel
    {
        public long VisitId { get; set; }
        public long? RegId { get; set; }
      
        public long? ConsultantDocId { get; set; }
       
        public long? DepartmentId { get; set; }
       
    }
    public class ConsRefDoctorModelValidator : AbstractValidator<ConsRefDoctorModel>
    {
        public ConsRefDoctorModelValidator()
        {
            RuleFor(x => x.VisitId).NotNull().NotEmpty().WithMessage("VisitId is required");
            RuleFor(x => x.ConsultantDocId).NotNull().NotEmpty().WithMessage("ConsultantDocId is required");
        
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId is required");
           
        }
    }
}
