using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class VechicleModel
    {
        public long VehicleId { get; set; }
        public string VehicleName { get; set; } = null!;
        public string VehicleNo { get; set; } = null!;
        public string? VehicleModel { get; set; }
        public DateTime? ManuDate { get; set; }
        public string? VehicleType { get; set; }
        public string? Note { get; set; }

    }

    public class VehicleMasterModelValidator : CommonDynamicValidator<VechicleModel>
    {
        public VehicleMasterModelValidator()
        {
            RuleFor(x => x.VehicleName).NotNull().NotEmpty().WithMessage("VehicleName is required");
            RuleFor(x => x.VehicleNo).NotNull().NotEmpty().WithMessage("VehicleNo is required");

        }
    }
}
