using HIMS.API.Models.OPPatient;
using HIMS.API.Models.Pathology;
using static HIMS.API.Models.OutPatient.AppointmentBillModel;

namespace HIMS.API.Models.OutPatient
{
    public class AppointmentBillModel
    {

        public class AppRegistrationBill1
        {
            public DateTime? RegDate { get; set; }
            public string? RegTime { get; set; }
            public long? PrefixId { get; set; }
            public string? FirstName { get; set; }
            public string? MiddleName { get; set; }
            public string? LastName { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? PinNo { get; set; }
            public DateTime? DateofBirth { get; set; }
            public string? Age { get; set; }
            public long? GenderId { get; set; }
            public string? PhoneNo { get; set; }
            public string? MobileNo { get; set; }
            public long? AddedBy { get; set; }
            public string? AgeYear { get; set; }
            public string? AgeMonth { get; set; }
            public string? AgeDay { get; set; }
            public long? CountryId { get; set; }
            public long? StateId { get; set; }
            public long? CityId { get; set; }
            public long? MaritalStatusId { get; set; }
            public bool? IsCharity { get; set; }
            public long? ReligionId { get; set; }
            public long? AreaId { get; set; }
            public bool? IsSeniorCitizen { get; set; }
            public string? AadharCardNo { get; set; }
            public string? PanCardNo { get; set; }
            public string? Photo { get; set; }
            public string? EmgContactPersonName { get; set; }
            public long? EmgRelationshipId { get; set; }
            public string? EmgMobileNo { get; set; }
            public string? EmgLandlineNo { get; set; }
            public string? EngAddress { get; set; }
            public string? EmgAadharCardNo { get; set; }
            public string? EmgDrivingLicenceNo { get; set; }
            public string? MedTourismPassportNo { get; set; }
            public DateTime? MedTourismVisaIssueDate { get; set; }
            public DateTime? MedTourismVisaValidityDate { get; set; }
            public string? MedTourismNationalityId { get; set; }
            public long? MedTourismCitizenship { get; set; }
            public string? MedTourismPortOfEntry { get; set; }
            public DateTime? MedTourismDateOfEntry { get; set; }
            public string? MedTourismResidentialAddress { get; set; }
            public string? MedTourismOfficeWorkAddress { get; set; }
            public long RegId { get; set; }


        }
        public class AppBillingMainModels
        {
            public AppRegistrationBill1 AppRegistrationBills { get; set; }
            public AppVisitDetailModel1 Visit { get; set; }
            public AppOPBillIngModels AppOPBillIngModels { get; set; }

        }


        public class AppOPBillIngModels
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

        public class AppVisitDetailModel1
        {
            public long? RegId { get; set; }
            public DateTime? VisitDate { get; set; }
            public string? VisitTime { get; set; }
            public long? UnitId { get; set; }
            public long? PatientTypeId { get; set; }
            public long? ConsultantDocId { get; set; }
            public long? RefDocId { get; set; }
            public long? TariffId { get; set; }
            public long? CompanyId { get; set; }
            public int? AddedBy { get; set; }
            public int? UpdatedBy { get; set; }
            public long? IsCancelledBy { get; set; }
            public bool? IsCancelled { get; set; }
            public DateTime? IsCancelledDate { get; set; }
            public long? ClassId { get; set; }
            public long? DepartmentId { get; set; }
            public long? PatientOldNew { get; set; }
            public long? FirstFollowupVisit { get; set; }
            public long? AppPurposeId { get; set; }
            public DateTime? FollowupDate { get; set; }
            public byte? CrossConsulFlag { get; set; }
            public long? PhoneAppId { get; set; }
            public long? CampId { get; set; }
            public long? CrossConsultantDrId { get; set; }
            public long VisitId { get; set; }

        }
    }
}