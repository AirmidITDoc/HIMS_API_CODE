using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class VillageMasterModel
    {
        public int VillageId { get; set; }
        public string? VillageName { get; set; }
        public string? TalukaName { get; set; }
    }
    public class VillageMasterModelValidator : AbstractValidator<VillageMasterModel>
    {
        public VillageMasterModelValidator()
        {
            RuleFor(x => x.VillageName).NotNull().NotEmpty().WithMessage("VillageName is required");
            RuleFor(x => x.TalukaName).NotNull().NotEmpty().WithMessage("TalukaName  is required");
        }
    }
}
