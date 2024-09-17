using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class GroupMasterModel
    {
        public long GroupId { get; set; }
        public string? GroupName { get; set; }
    }

    public class GroupMasterModelValidator : AbstractValidator<GroupMasterModel>
    {
        public GroupMasterModelValidator()
        {
            RuleFor(x => x.GroupName).NotNull().NotEmpty().WithMessage("GroupName is required");
        }
    }
}
