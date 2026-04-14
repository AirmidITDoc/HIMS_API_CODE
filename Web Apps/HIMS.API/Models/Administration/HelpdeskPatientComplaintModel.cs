using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Administration
{
    public class HelpdeskPatientComplaintModel
    {
        public int ComplaintId { get; set; }
        public string PatientName { get; set; } = null!;
        public string RegId { get; set; } = null!;
        public int? OpdipdNo { get; set; }
        public int? DepartmentId { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }
        public string? Complaint { get; set; }
        public DateTime? ComplaintDate { get; set; }
        public string? ComplaintTime { get; set; }
        public bool? IsDischarge { get; set; }
    }
    public class HelpdeskPatientComplaintModelValidator : AbstractValidator<HelpdeskPatientComplaintModel>
    {
        public HelpdeskPatientComplaintModelValidator()
        {
            RuleFor(x => x.ComplaintDate).NotNull().NotEmpty().WithMessage("ComplaintDate  is required");
            RuleFor(x => x.PatientName).NotNull().NotEmpty().WithMessage("PatientName  is required");
            RuleFor(x => x.ComplaintTime).NotNull().NotEmpty().WithMessage("ComplaintTime  is required");
            RuleFor(x => x.MobileNo).NotNull().NotEmpty().WithMessage("MobileNo  is required");

        }
    }
}
