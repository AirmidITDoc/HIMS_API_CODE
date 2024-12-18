using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class HospitalMasterModel
    {
        public long HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
    }
    public class HospitalMasterModelModelValidator : AbstractValidator<HospitalMasterModel>
    {
        public HospitalMasterModelModelValidator()
        {
            RuleFor(x => x.HospitalName).NotNull().NotEmpty().WithMessage("HospitalName is required");
            RuleFor(x => x.HospitalAddress).NotNull().NotEmpty().WithMessage("HospitalAddress is required");
        }
    }
}
