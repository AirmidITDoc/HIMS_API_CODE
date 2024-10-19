using FluentValidation;

namespace HIMS.API.Models.OPPatient
{
    public class PhoneAppointmentModel
    {
        public long PhoneAppId { get; set; }
        public DateTime? AppDate { get; set; }
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
    public class PhoneAppointmentModelValidator : AbstractValidator<PhoneAppointmentModel>
    {
        public PhoneAppointmentModelValidator()
        {
            RuleFor(x => x.AppDate).NotNull().NotEmpty().WithMessage("AppDate is required");
            RuleFor(x => x.AppTime).NotNull().NotEmpty().WithMessage("AppTime is required");
        }
    }

}

