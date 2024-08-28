using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class WardMasterModel
    {
        public long RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? RoomType { get; set; }
        public long? LocationId { get; set; }
        public bool? IsAvailible { get; set; }
        public long? ClassId { get; set; }
    }

    public class WardMasterModelValidator : AbstractValidator<WardMasterModel>
    {
        public WardMasterModelValidator()
        {
            RuleFor(x => x.RoomName).NotNull().NotEmpty().WithMessage("RoomName  is required");
            RuleFor(x => x.RoomType).NotNull().NotEmpty().WithMessage(" RoomType is required");
            RuleFor(x => x.LocationId).NotNull().NotEmpty().WithMessage(" LocationId is required");
            RuleFor(x => x.IsAvailible).NotNull().NotEmpty().WithMessage(" IsAvailible is required");
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");



        }
    }
}
