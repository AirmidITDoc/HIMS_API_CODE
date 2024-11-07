using FluentValidation;
using HIMS.API.Models.OutPatient;

namespace HIMS.API.Models.OPPatient
{
    public class PhoneAppListModel
    {
        public long PhoneAppId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? PhAppDate { get; set; }
        public DateTime? PhAppTime { get; set; }
        public long? DoctorId { get; set; }

    }
    public class PhoneAppListModelValidator : AbstractValidator<PhoneAppListModel>
    {
        public PhoneAppListModelValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.PhAppDate).NotNull().NotEmpty().WithMessage("PhAppDate is required");
            RuleFor(x => x.PhAppTime).NotNull().NotEmpty().WithMessage("PhAppTime is required");
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId is required");
            
        }
    }

}
