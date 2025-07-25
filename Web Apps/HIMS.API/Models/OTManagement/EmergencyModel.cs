using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.OTManagement
{
    public class EmergencyModel
    {
        public long EmgId { get; set; }
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
        public long? PrefixId { get; set; }
        public long? GenderId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public int? AgeYear { get; set; }
        public int? AgeMonth { get; set; }
        public int? AgeDay { get; set; }
        public string? Comment { get; set; }
        public int? Classid { get; set; }
        public int? Tariffid { get; set; }

    }
    public class EmergencyModeValidator : AbstractValidator<EmergencyModel>
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
        public long? PrefixId { get; set; }
        public long? GenderId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public int? AgeYear { get; set; }
        public int? AgeMonth { get; set; }
        public int? AgeDay { get; set; }
        public string? MobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Comment { get; set; }
        public int? Classid { get; set; }
        public int? Tariffid { get; set; }



    }
    public class EmergencyCancel
    {

        public long EmgId { get; set; }

    }
    public class GetEmergencyModel
    {
        public long EmgId { get; set; }
        public long? RegId { get; set; }
        public DateTime? EmgDate { get; set; }
        public DateTime? EmgTime { get; set; }
        public string? SeqNo { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public int? AgeYear { get; set; }
        public int? AgeMonth { get; set; }
        public int? AgeDay { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public string? Comment { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? Classid { get; set; }
        public int? Tariffid { get; set; }

    }

    public class UpdateAddChargesFromEmergency
    {
        public long EmgId {  get; set; } 
        public long NewAdmissionId {  get; set; }

    }
}
