using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class GenericMasterModel
    {
        public long ItemGenericNameId { get; set; }
        public string? ItemGenericName { get; set; }
    }
    public class GenericMasterModelValidator : AbstractValidator<GenericMasterModel>
    {
        public GenericMasterModelValidator()
        {
            RuleFor(x => x.ItemGenericName).NotNull().NotEmpty().WithMessage("ItemGenericName is required");
        }
    }
}
