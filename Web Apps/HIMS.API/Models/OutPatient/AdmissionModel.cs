using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class AdmissionRegModel
    {
        public long RegId { get; set; }
        public string? RegDate { get; set; }
        public string? RegTime { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string? DateofBirth { get; set; }
        public string? Age { get; set; }
        public long? GenderID { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }

        public int? AddedBy { get; set; }

        public int? UpdatedBy { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public long? MaritalStatusId { get; set; }
        public bool? IsCharity { get; set; }
        //public string? RegPrefix { get; set; }
        public long? ReligionId { get; set; }
        public long? AreaId { get; set; }
        //public decimal? AnnualIncome { get; set; }
        //public bool? IsIndientOrWeaker { get; set; }
        //public string? RationCardNo { get; set; }
        //public bool? IsMember { get; set; }
        public bool? IsSeniorCitizen { get; set; }
        public string? AadharCardNo { get; set; }
        public string? PanCardNo { get; set; }
        public string? Photo { get; set; }

    }
    public class AdmissionModelValidator : AbstractValidator<AdmissionRegModel>
    {
        public AdmissionModelValidator()
        {
            RuleFor(x => x.PrefixId).NotNull().NotEmpty().WithMessage("Prefix is required");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.GenderID).NotNull().NotEmpty().WithMessage("Gender is required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("City is required");

            RuleFor(x => x.StateId).NotNull().NotEmpty().WithMessage("State is required");
            RuleFor(x => x.CountryId).NotNull().NotEmpty().WithMessage("Country is required");
        }
    }
    public class ADMISSIONModel
    {
        public long AdmissionId { get; set; }
        public long? RegId { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? AdmissionTime { get; set; }
        public long? PatientTypeId { get; set; }
        public long? HospitalId { get; set; }
        public long? DocNameId { get; set; }
        public long? RefDocNameId { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public byte? IsDischarged { get; set; }
        public byte? IsBillGenerated { get; set; }
        public string? Ipdno { get; set; }
        public long? IsCancelled { get; set; }
        public long? CompanyId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public long? DepartmentId { get; set; }
        public string? RelativeName { get; set; }
        public string? RelativeAddress { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public long? RelationshipId { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsMlc { get; set; }
        public string? MotherName { get; set; }
        public long? AdmittedDoctor1 { get; set; }
        public long? AdmittedDoctor2 { get; set; }
        public string? IsProcessing { get; set; }
        public bool? Ischarity { get; set; }
        public long? RefByTypeId { get; set; }
        public long? RefByName { get; set; }
        public bool? IsMarkForDisNur { get; set; }
        public long? IsMarkForDisNurId { get; set; }
        public DateTime? IsMarkForDisNurDateTime { get; set; }
        public bool? IsCovidFlag { get; set; }
        public long? IsCovidUserId { get; set; }
        public DateTime? IsCovidUpdateDate { get; set; }
        public long? IsUpdatedBy { get; set; }
        public long? SubTpaComId { get; set; }
        public string? PolicyNo { get; set; }
        public decimal? AprovAmount { get; set; }
        public DateTime? CompDod { get; set; }
        public bool? IsPharClearance { get; set; }
        public long? Ipnumber { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? HosApreAmt { get; set; }
        public decimal? PathApreAmt { get; set; }
        public decimal? PharApreAmt { get; set; }
        public decimal? RadiApreAmt { get; set; }
        public float? PharDisc { get; set; }
        public long? CompBillNo { get; set; }
        public DateTime? CompBillDate { get; set; }
        public decimal? CompDiscount { get; set; }
        public DateTime? CompDisDate { get; set; }
        public long? CBillNo { get; set; }
        public decimal? CFinalBillAmt { get; set; }
        public decimal? CDisallowedAmt { get; set; }
        public string? ClaimNo { get; set; }
        public decimal? HdiscAmt { get; set; }
        public decimal? COutsideInvestAmt { get; set; }
        public decimal? RecoveredByPatient { get; set; }
        public decimal? HChargeAmt { get; set; }
        public decimal? HAdvAmt { get; set; }
        public long? HBillId { get; set; }
        public DateTime? HBillDate { get; set; }
        public string? HBillNo { get; set; }
        public decimal? HTotalAmt { get; set; }
        public decimal? HDiscAmt1 { get; set; }
        public decimal? HNetAmt { get; set; }
        public decimal? HPaidAmt { get; set; }
        public decimal? HBalAmt { get; set; }
        public bool? IsOpToIpconv { get; set; }
        public string? RefDoctorDept { get; set; }
        public byte? AdmissionType { get; set; }
        public decimal? MedicalApreAmt { get; set; }

    }
    public class AdmissModelValidator : AbstractValidator<ADMISSIONModel>
    {
        public AdmissModelValidator()
        {
           // RuleFor(x => x.RegId).NotNull().NotEmpty().WithMessage("regId is required");
            RuleFor(x => x.HospitalId).NotNull().NotEmpty().WithMessage("UnitId is required");
            RuleFor(x => x.PatientTypeId).NotNull().NotEmpty().WithMessage("PatientTypeId is required");
            //RuleFor(x => x.ConsultantDocId).NotNull().NotEmpty().WithMessage("ConsultantDocId is required");
            //RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
            //RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is required");
            //RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId is required");
            //RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
        }
    }
    public class NewAdmission
    {
        public AdmissionRegModel AdmissionReg { get; set; }
        public ADMISSIONModel ADMISSION { get; set; }
    }

}

