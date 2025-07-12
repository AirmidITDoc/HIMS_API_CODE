using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.OTManagement
{
    public class EmergencyMode
    {
        public long? RegId { get; set; }
        public DateTime? EmgDate { get; set; }
        public string? EmgTime { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? prefixId { get; set; }
        public long? GenderId { get; set; }
        public long? CityId { get; set; }
        public long? AgeYear { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long EmgId { get; set; }

    }
    public class EmergencyModeValidator : AbstractValidator<EmergencyMode>
    {
        public EmergencyModeValidator()
        {
            RuleFor(x => x.EmgDate).NotNull().NotEmpty().WithMessage("EmgDate is required");
            RuleFor(x => x.EmgTime).NotNull().NotEmpty().WithMessage("EmgTime  is required");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage(" FirstName required");
          

        }
    }
    public class EmergencyupdateModel
    {
        public long EmgId { get; set; }
        public long? RegId { get; set; }
        public DateTime? EmgDate { get; set; }
        public string? EmgTime { get; set; }
        public long? prefixId { get; set; }
        public long? GenderId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? AgeYear { get; set; }
        public string? MobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? UpdatedBy { get; set; }


    }
    public class EmergencyCancel
    {

        public long EmgId { get; set; }
        //public long IsCanceledBy { get; set; }

    }
}
