using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class TalukaMasterModel
    {
        public long TalukaId { get; set; }
        public string? TalukaName { get; set; }
        public long? CityId { get; set; }
    }
    public class TalukaMasterModelModelValidator : AbstractValidator<TalukaMasterModel>
    {
        public TalukaMasterModelModelValidator()
        {
            RuleFor(x => x.TalukaName).NotNull().NotEmpty().WithMessage("TalukaName is required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("CityId is required");
        }
    }
}
