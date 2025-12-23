using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class UnitOfMeasurementModel
    {
        public long UnitofMeasurementId { get; set; }
        public string? UnitofMeasurementName { get; set; }
    }

    public class UnitOfMeasurementModelValidator : AbstractValidator<UnitOfMeasurementModel>
    {
        public UnitOfMeasurementModelValidator()
        {
            RuleFor(x => x.UnitofMeasurementName).NotNull().NotEmpty().WithMessage("UnitofMeasurementName is required");
        }
    }


}
