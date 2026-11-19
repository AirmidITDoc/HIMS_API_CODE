using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class RoleTemplateModel
    {
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
    }
    public class RoleTemplateModelValidator : CommonDynamicValidator<RoleTemplateModel>
    {
        public RoleTemplateModelValidator()
        {
            RuleFor(x => x.RoleName).NotNull().NotEmpty().WithMessage("RoleName  is required");
        }
    }
}
