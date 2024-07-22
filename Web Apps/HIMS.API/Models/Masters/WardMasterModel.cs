using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class WardMasterModel
    {
        public long RoomId { get; set; }
        public string? RoomName { get; set; }
    }

    public class WardMasterModelValidator : AbstractValidator<WardMasterModel>
    {
        public WardMasterModelValidator()
        {
            RuleFor(x => x.RoomName).NotNull().NotEmpty().WithMessage("Ward Master is required");
        }
    }
}
