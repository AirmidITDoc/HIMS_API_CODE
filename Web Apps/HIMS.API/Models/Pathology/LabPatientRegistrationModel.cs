using FluentValidation;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.Pathology
{
  
    public class LabPatientRegistrationModels
    {
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
        public int? CreatedBy { get; set; }
        public long LabPatientId { get; set; }

        public List<TLabTestRequestModel> TLabTestRequests { get; set; }

    }
    public class LabPatientRegistrationModelsValidator : AbstractValidator<LabPatientRegistrationModels>
    {
        public LabPatientRegistrationModelsValidator()
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
        public int? CreatedBy { get; set; }

    }
    public class TLabTestRequestModelValidator : AbstractValidator<TLabTestRequestModel>
    {
        public TLabTestRequestModelValidator()
        {
            RuleFor(x => x.LabServiceId).NotNull().NotEmpty().WithMessage("LabServiceId is required");
            RuleFor(x => x.TotalAmount).NotNull().NotEmpty().WithMessage("TotalAmount is required");


        }
    }
    public class LabPatientRegistrationModel
    {
        public DateTime? RegDate { get; set; }
        public string? RegTime { get; set; }
        public long? UnitId { get; set; }
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
        public int? CreatedBy { get; set; }
        public long LabPatientId { get; set; }


    }
    public class OPBillIngLabModel
    {
        public int? OpdIpdId { get; set; }
        public long? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? Ipdno { get; set; }
        public long? AgeYear { get; set; }
        public long? AgeMonth { get; set; }
        public long? AgeDays { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public bool? PatientType { get; set; }
        public string? CompanyName { get; set; }
        public decimal? CompanyAmt { get; set; }
        public decimal? PatientAmt { get; set; }
        public float? TotalAmt { get; set; }
        public float? ConcessionAmt { get; set; }
        public float? NetPayableAmt { get; set; }
        public float? PaidAmt { get; set; }
        public float? BalanceAmt { get; set; }
        public string? BillDate { get; set; }
        public int? OpdIpdType { get; set; }
        public int? AddedBy { get; set; }
        public float? TotalAdvanceAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public string? BillTime { get; set; }
        public int? ConcessionReasonId { get; set; }
        public bool? IsSettled { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsFree { get; set; }
        public int? CompanyId { get; set; }
        public int? TariffId { get; set; }
        public int? UnitId { get; set; }
        public int? InterimOrFinal { get; set; }
        public int? CompanyRefNo { get; set; }
        public int? ConcessionAuthorizationName { get; set; }
        public float? SpeTaxPer { get; set; }
        public float? SpeTaxAmt { get; set; }
        public int? CompDiscAmt { get; set; }
        public string? DiscComments { get; set; }
        public long? CashCounterId { get; set; }
        public long? CreatedBy { get; set; }
        public int BillNo { get; set; }
        public List<ChargesModel> AddCharges { get; set; }
        public List<BillDetailsModel> BillDetails { get; set; }
        public List<Packcagechargesmodel?> Packcagecharges { get; set; }
        public OPPaymentModel? Payments { get; set; }
    }
    public class LabRegistrationModels
    {
        public LabPatientRegistrationModel LabPatientRegistration {  get; set; }
        //public List<TLabTestRequestModel> TLabTestRequest { get; set; }
        public OPBillIngLabModel OPBillIngModels { get; set; }
        //public List<ChargesModel> AddCharges { get; set; }
        //public List<BillDetailsModel> BillDetails { get; set; }
        //public List<Packcagechargesmodel?> Packcagecharges { get; set; }
        //public OPPaymentModel? Payments { get; set; }

    }
}
