using FluentValidation;


namespace HIMS.API.Models.OPPatient
{
    public class RefDoctorModel
    {
        public long VisitId { get; set; }
        public long? RegId { get; set; }
      
        public long? RefDocId { get; set; }
      
        //public long? DepartmentId { get; set; }
       
    }
    public class RefDoctorModelValidator : AbstractValidator<RefDoctorModel>
    {
        public RefDoctorModelValidator()
        {
            RuleFor(x => x.VisitId).NotNull().NotEmpty().WithMessage("VisitId is required");
         
            RuleFor(x => x.RefDocId).NotNull().NotEmpty().WithMessage("RefDocId is required");
            //RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId is required");
           
        }
    }
}
