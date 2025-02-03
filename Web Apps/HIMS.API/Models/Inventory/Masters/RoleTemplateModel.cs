using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory.Masters
{
    public class RoleTemplateModel
    {
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
    }
    public class RoleTemplateModelValidator : AbstractValidator<RoleTemplateModel>
    {
        public RoleTemplateModelValidator()
        {
            RuleFor(x => x.RoleName).NotNull().NotEmpty().WithMessage("RoleName  is required");
        }
    }
}
