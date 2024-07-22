using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.Masters
{
    public class BedMasterModel
    {
        public int BedId { get; set; }
        public string BedName { get; set; } = string.Empty;
    }

    public class BedMasterModelValidator : AbstractValidator<BedMasterModel>
    {
        public BedMasterModelValidator()
        {
            RuleFor(x => x.BedName).NotNull().NotEmpty().WithMessage("Bed  is required");
        }
    }
}
