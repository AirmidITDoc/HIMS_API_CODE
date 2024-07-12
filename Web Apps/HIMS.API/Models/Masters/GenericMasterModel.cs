using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class GenericMasterModel
    {
        public long GenericId { get; set; }
        public string? GenericName { get; set; }
    }
    public class GenericMasterModelValidator : AbstractValidator<GenericMasterModel>
    {
        public GenericMasterModelValidator()
        {
            RuleFor(x => x.GenericName).NotNull().NotEmpty().WithMessage("Generic is required");
        }
    }
}
