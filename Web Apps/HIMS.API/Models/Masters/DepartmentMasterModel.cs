using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class DepartmentMasterModel
    {
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }

    public class DepartmentMasterModelValidator : CommonDynamicValidator<DepartmentMasterModel>
    {
        public DepartmentMasterModelValidator()
        {
            RuleFor(x => x.DepartmentName).NotNull().NotEmpty().WithMessage("DepartmentName is required");
        }
    }
}
