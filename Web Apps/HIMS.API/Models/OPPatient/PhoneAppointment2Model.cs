using FluentValidation;
using HIMS.API.Models.OutPatient;

namespace HIMS.API.Models.OPPatient
{
    public class PhoneAppointment2Model
    {
        public long PhoneAppId { get; set; }
        public string? AppDate { get; set; }
        public string? AppTime { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? PhAppDate { get; set; }
        public string? PhAppTime { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? RegNo { get; set; }
    }
    public class PhoneAppointment2ModelValidator : AbstractValidator<PhoneAppointment2Model>
    {
        public PhoneAppointment2ModelValidator()
        {
            RuleFor(x => x.AppDate).NotNull().NotEmpty().WithMessage("AppDate is required");
            RuleFor(x => x.AppTime).NotNull().NotEmpty().WithMessage("AppTime is required");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");

        }
    }
    public class PhoneAppointmentCancel
    {
        public int PhoneAppId { get; set; }
    }

}
