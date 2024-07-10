using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DepartmentMasterModel
    {
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }

    public class DepartmentMasterModelValidator : AbstractValidator<DepartmentMasterModel>
    {
        public DepartmentMasterModelValidator()
        {
            RuleFor(x => x.DepartmentName).NotNull().NotEmpty().WithMessage("Department is required");
        }
    }
}
