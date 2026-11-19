using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class RelationshipMasterModel
    {
        public long RelationshipId { get; set; }
        public string? RelationshipName { get; set; }

    }

    public class RelationshipMasterModelValidator : CommonDynamicValidator<RelationshipMasterModel>
    {
        public RelationshipMasterModelValidator()
        {
            RuleFor(x => x.RelationshipName).NotNull().NotEmpty().WithMessage("Religion is required");
        }
    }
}
