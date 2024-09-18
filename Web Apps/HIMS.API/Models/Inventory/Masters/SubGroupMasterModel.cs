using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SubGroupMasterModel
    {
        public long SubGroupId { get; set; }
        public long? GroupId { get; set; }
        public string? SubGroupName { get; set; }
    }

    public class SubGroupMasterModelValidator : AbstractValidator<SubGroupMasterModel>
    {
        public SubGroupMasterModelValidator()
        {
            RuleFor(x => x.SubGroupName).NotNull().NotEmpty().WithMessage("SubGroupName is required");
        }
    }
}
