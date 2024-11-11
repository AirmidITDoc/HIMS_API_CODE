using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory.Masters
{
    public class PrefixMasterModel
    {
        public long PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public long? SexId { get; set; }

    }
    public class PrefixMasterModelValidator : AbstractValidator<PrefixMasterModel>
    {
        public PrefixMasterModelValidator()
        {
            RuleFor(x => x.PrefixName).NotNull().NotEmpty().WithMessage("PrefixName  is required");
            RuleFor(x => x.SexId).NotNull().NotEmpty().WithMessage("SexId  is required");

        }
    }
}
