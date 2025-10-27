using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class RoleMasterModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
    public class RoleMasterModelModelValidator : AbstractValidator<RoleMasterModel>
    {
        public RoleMasterModelModelValidator()
        {
            // RuleFor(x => x.RoleId).NotNull().NotEmpty().WithMessage("RoleId is required");
            RuleFor(x => x.RoleName).NotNull().NotEmpty().WithMessage("RoleName is required");
        }
    }
}
