using FluentValidation;
using HIMS.API.Models.Inventory.Masters;

namespace HIMS.API.Models.Pathology
{
    public class LabPatientRegistrationModel
    {
        public long LabPatientId { get; set; }
        public DateTime? RegDate { get; set; }
        public string? RegTime { get; set; }
        public long? UnitId { get; set; }
        //public string? LabRequestNo { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? RefDocId { get; set; }
        public List<TLabTestRequestModel> TLabTestRequests { get; set; }

    }
    public class LabPatientRegistrationModelValidator : AbstractValidator<LabPatientRegistrationModel>
    {
        public LabPatientRegistrationModelValidator()
        {
            RuleFor(x => x.RegDate).NotNull().NotEmpty().WithMessage("RegDate is required");
            RuleFor(x => x.RegTime).NotNull().NotEmpty().WithMessage("RegTime is required");


        }
    }
    public class TLabTestRequestModel
    {
        public long LabTestRequestId { get; set; }
        public long? LabPatientId { get; set; }
        public long? LabServiceId { get; set; }
        public decimal? Price { get; set; }
        public long? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
    }
    public class TLabTestRequestModelValidator : AbstractValidator<TLabTestRequestModel>
    {
        public TLabTestRequestModelValidator()
        {
            RuleFor(x => x.LabServiceId).NotNull().NotEmpty().WithMessage("LabServiceId is required");
            RuleFor(x => x.TotalAmount).NotNull().NotEmpty().WithMessage("TotalAmount is required");


        }
    }
}
