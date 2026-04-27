using FluentValidation;

namespace HIMS.API.Models.Employee
{
    public class EmployeeDesignationModel
    {
        public long EmpDesignationId { get; set; }
        public string? EmpDesignationName { get; set; }
    }
    public class EmployeeDesignationModelValidator : AbstractValidator<EmployeeDesignationModel>
    {
        public EmployeeDesignationModelValidator()
        {
            RuleFor(x => x.EmpDesignationName).NotNull().NotEmpty().WithMessage("EmpDesignationName  is required");
        }
    }
}
