using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class GenericMasterModel
    {
        public long ItemGenericNameId { get; set; }
        public string? ItemGenericName { get; set; }
    }
    public class GenericMasterModelValidator : CommonDynamicValidator<GenericMasterModel>
    {
        public GenericMasterModelValidator()
        {
            RuleFor(x => x.ItemGenericName).NotNull().NotEmpty().WithMessage("ItemGenericName is required");
        }
    }
}
