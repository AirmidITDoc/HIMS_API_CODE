using FluentValidation;
using HIMS.API.Models.Inventory;


namespace HIMS.API.Models.IPPatient
{
    public class DischargeModel
    {
        public long DischargeId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public DateTime? DischargeTime { get; set; }
        //public long? IsCancelled { get; set; }
        public long? DischargeTypeId { get; set; }
        public long? DischargedDocId { get; set; }
        public long? DischargedRmoid { get; set; }

        public long? AddedBy { get; set; }
        public List<BedMasterModel1> BedMasters { get; set; }
        public List<AdmissionModel> Admission { get; set; }
        //public long? UpdatedBy { get; set; }
        //public long? IsCancelledby { get; set; }
        //public DateTime? IsCancelledDate { get; set; }
        //public bool? IsMrdreceived { get; set; }
        //public DateTime? MrdreceivedDate { get; set; }
        //public DateTime? MrdreceivedTime { get; set; }
        //public long? MrdreceivedUserId { get; set; }
        //public string? MrdreceivedName { get; set; }

    }
    public class DischargeModelValidator : AbstractValidator<DischargeModel>
    {
        public DischargeModelValidator()
        {
            RuleFor(x => x.DischargeDate).NotNull().NotEmpty().WithMessage("DischargeDate is required");
            RuleFor(x => x.AdmissionId).NotNull().NotEmpty().WithMessage("AdmissionId is required");
            RuleFor(x => x.DischargeTypeId).NotNull().NotEmpty().WithMessage("DischargeTypeId is required");


        }
    }
    public class BedMasterModel1
    {
        public long BedId { get; set; }
        ////public string? BedName { get; set; }
        ////public long? RoomId { get; set; }
        ////public bool? IsAvailible { get; set; }
        ////public bool? IsActive { get; set; }
        ////public int? CreatedBy { get; set; }
        ////public DateTime? CreatedDate { get; set; }
        ////public int? ModifiedBy { get; set; }
        ////public DateTime? ModifiedDate { get; set; }

    }
    public class BedMasterModel1Validator : AbstractValidator<BedMasterModel1>
    {
        public BedMasterModel1Validator()
        {
            RuleFor(x => x.BedId).NotNull().NotEmpty().WithMessage("BedId is required");
            //RuleFor(x => x.RoomId).NotNull().NotEmpty().WithMessage("RoomId is required");



        }
    }

    public class AdmissionModel
    {
        public long AdmissionId { get; set; }
        //public long? RegId { get; set; }
        //public DateTime? AdmissionDate { get; set; }
        //public DateTime? AdmissionTime { get; set; }
        //public long? PatientTypeId { get; set; }
        //public long? HospitalId { get; set; }
        //public long? DocNameId { get; set; }
        //public long? RefDocNameId { get; set; }
        //public long? WardId { get; set; }
        //public long? BedId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public DateTime? DischargeTime { get; set; }
        public byte? IsDischarged { get; set; }
        //public byte? IsBillGenerated { get; set; }
        //public string? Ipdno { get; set; }
        //public long? IsCancelled { get; set; }
        //public long? CompanyId { get; set; }
        //public long? TariffId { get; set; }
        //public long? ClassId { get; set; }
        //public long? DepartmentId { get; set; }
        //public string? RelativeName { get; set; }
        //public string? RelativeAddress { get; set; }
        //public string? PhoneNo { get; set; }
        //public string? MobileNo { get; set; }
        //public long? RelationshipId { get; set; }
        //public long? AddedBy { get; set; }
        //public bool? IsMlc { get; set; }
        //public string? MotherName { get; set; }
        //public long? AdmittedDoctor1 { get; set; }
        //public long? AdmittedDoctor2 { get; set; }
        //public string? IsProcessing { get; set; }
        //public bool? Ischarity { get; set; }
        //public long? RefByTypeId { get; set; }
        //public long? RefByName { get; set; }
        //public bool? IsMarkForDisNur { get; set; }
        //public long? IsMarkForDisNurId { get; set; }
        //public DateTime? IsMarkForDisNurDateTime { get; set; }
        //public bool? IsCovidFlag { get; set; }
        //public long? IsCovidUserId { get; set; }
        //public DateTime? IsCovidUpdateDate { get; set; }
        //public long? IsUpdatedBy { get; set; }
        //public long? SubTpaComId { get; set; }
        //public string? PolicyNo { get; set; }
        //public decimal? AprovAmount { get; set; }
        //public DateTime? CompDod { get; set; }
        //public bool? IsPharClearance { get; set; }
        //public long? Ipnumber { get; set; }
        //public decimal? EstimatedAmount { get; set; }
        //public decimal? ApprovedAmount { get; set; }
        //public decimal? HosApreAmt { get; set; }
        //public decimal? PathApreAmt { get; set; }
        //public decimal? PharApreAmt { get; set; }
        //public decimal? RadiApreAmt { get; set; }
        //public float? PharDisc { get; set; }
        //public long? CompBillNo { get; set; }
        //public DateTime? CompBillDate { get; set; }
        //public decimal? CompDiscount { get; set; }
        //public DateTime? CompDisDate { get; set; }
        //public long? CBillNo { get; set; }
        //public decimal? CFinalBillAmt { get; set; }
        //public decimal? CDisallowedAmt { get; set; }
        //public string? ClaimNo { get; set; }
        //public decimal? HdiscAmt { get; set; }
        //public decimal? COutsideInvestAmt { get; set; }
        //public decimal? RecoveredByPatient { get; set; }
        //public decimal? HChargeAmt { get; set; }
        //public decimal? HAdvAmt { get; set; }
        //public long? HBillId { get; set; }
        //public DateTime? HBillDate { get; set; }
        //public string? HBillNo { get; set; }
        //public decimal? HTotalAmt { get; set; }
        //public decimal? HDiscAmt1 { get; set; }
        //public decimal? HNetAmt { get; set; }
        //public decimal? HPaidAmt { get; set; }
        //public decimal? HBalAmt { get; set; }
        //public bool? IsOpToIpconv { get; set; }
        //public string? RefDoctorDept { get; set; }
        //public byte? AdmissionType { get; set; }
        //public decimal? MedicalApreAmt { get; set; }

    }
    public class AdmissionModelValidator : AbstractValidator<AdmissionModel>
    {
        public AdmissionModelValidator()
        {
            RuleFor(x => x.DischargeDate).NotNull().NotEmpty().WithMessage("DischargeDate is required");




        }
    }
    //public class DISCHARGEModel
    //{
    //    public DischargeModel DischargeModel { get; set; }
    //    public BedMasterModel1 BedMasterModel1 { get; set; }
    //    public AdmissionModel AdmissionModel { get; set; }
    //}

}

