using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DoctorExecutiveModel
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? EmployeId { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class DoctorExecutiveModelValidator : AbstractValidator<DoctorExecutiveModel>
    {
        public DoctorExecutiveModelValidator()
        {
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId is required");
            RuleFor(x => x.EmployeId).NotNull().NotEmpty().WithMessage("EmployeId is required");
        }
    }
    public class DoctorExecutiveUpdateModel
    {
        public long Id { get; set; }
        public long? DoctorId { get; set; }
        public long? EmployeId { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
