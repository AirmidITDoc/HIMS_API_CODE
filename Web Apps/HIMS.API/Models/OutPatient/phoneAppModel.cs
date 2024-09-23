using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class phoneAppModel
    {
        public long PhoneAppId { get; set; }
        public DateTime? AppDate { get; set; }
        public DateTime? AppTime { get; set; }
        public string? SeqNo { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? PhAppDate { get; set; }
        public DateTime? PhAppTime { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public string? RegNo { get; set; }
    }
    public class phoneAppModelValidator : AbstractValidator<phoneAppModel>
    {
        public phoneAppModelValidator()
        {
            RuleFor(x => x.AppDate).NotNull().NotEmpty().WithMessage("AppDate is required");
            RuleFor(x => x.AppTime).NotNull().NotEmpty().WithMessage("AppTime is required");
            RuleFor(x => x.SeqNo).NotNull().NotEmpty().WithMessage("SeqNo is required");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.MiddleName).NotNull().NotEmpty().WithMessage("MiddleName is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required");

        }
        public class PhoneAppointmentCancel
        {
            public int PhoneAppId { get; set; }
        }
    }
}
