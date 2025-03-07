using FluentValidation;


namespace HIMS.API.Models.OPPatient
{
    public class ConsRefDoctorModel
    {
        public long? ConsultantDocId { get; set; }
        public long? DepartmentId { get; set; }
        public long VisitId { get; set; }

    }
    public class ConsRefDoctorModelValidator : AbstractValidator<ConsRefDoctorModel>
    {
        public ConsRefDoctorModelValidator()
        {
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId is required");
            RuleFor(x => x.VisitId).NotNull().NotEmpty().WithMessage("VisitId is required");
           
        }
    }
    public class RefDoctorModel
    {
        public long? RefDocId { get; set; }
        public long VisitId { get; set; }


    }
    public class RefDoctorModelValidator : AbstractValidator<RefDoctorModel>
    {
        public RefDoctorModelValidator()
        {
            RuleFor(x => x.VisitId).NotNull().NotEmpty().WithMessage("VisitId is required");

            RuleFor(x => x.RefDocId).NotNull().NotEmpty().WithMessage("RefDocId is required");

        }
    }
    //public class RefDoctorsModel
    //{
    //    public ConsRefDoctorModel ConsRefDoctor { get; set; }
    //    //public RefDoctorModel RefDoctor { get; set; }

    //}
}
