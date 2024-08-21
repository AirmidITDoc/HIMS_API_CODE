using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class RelationshipMasterModel
    {
        public long RelationshipId { get; set; }
        public string? RelationshipName { get; set; }
        public bool? IsActive { get; set; }
        public long? AddBy { get; set; }
        public long? UpdatedBy { get; set; }

    }

    public class RelationshipMasterModelValidator : AbstractValidator<RelationshipMasterModel>
    {
        public RelationshipMasterModelValidator()
        {
            RuleFor(x => x.RelationshipName).NotNull().NotEmpty().WithMessage("Religion is required");
            RuleFor(x => x.IsActive).NotNull().NotEmpty().WithMessage("IsActive is required");
        }
    }
}
