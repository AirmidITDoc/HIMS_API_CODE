using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.Masters
{
    public class BedMasterModel
    {
        public long BedId { get; set; }
        public string? BedName { get; set; }
        public long? RoomId { get; set; }
        public bool? IsAvailible { get; set; }
    }

    public class BedMasterModelValidator : AbstractValidator<BedMasterModel>
    {
        public BedMasterModelValidator()
        {
            RuleFor(x => x.BedName).NotNull().NotEmpty().WithMessage("BedName  is required");
            RuleFor(x => x.RoomId).NotNull().NotEmpty().WithMessage("RoomId  is required");
            RuleFor(x => x.IsAvailible).NotNull().NotEmpty().WithMessage("IsAvailible  is required");


        }
    }
}
