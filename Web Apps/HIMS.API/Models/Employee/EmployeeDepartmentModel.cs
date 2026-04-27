using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Employee
{
    public class EmployeeDepartmentModel
    {
        public long EmpDepartmentId { get; set; }
        public string? EmpDepartmentName { get; set; }
     
    }
    public class EmployeeDepartmentModelValidator : AbstractValidator<EmployeeDepartmentModel>
    {
        public EmployeeDepartmentModelValidator()
        {
            RuleFor(x => x.EmpDepartmentName).NotNull().NotEmpty().WithMessage("EmpDepartmentName  is required");
        }
    }
}
