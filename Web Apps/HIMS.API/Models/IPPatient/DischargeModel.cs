using FluentValidation;
using HIMS.API.Models.Inventory;


namespace HIMS.API.Models.IPPatient
{
    public class DischargeModel
    {
        public long? AdmissionId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public long? DischargeTypeId { get; set; }
        public long? DischargedDocId { get; set; }
        public long? DischargedRmoid { get; set; }
        public long ModeOfDischargeId {  get; set; }
        public long? AddedBy { get; set; }
        public long DischargeId { get; set; }

    }
    public class DischargeModelValidator : AbstractValidator<DischargeModel>
    {
        public DischargeModelValidator()
        {
            RuleFor(x => x.DischargeDate).NotNull().NotEmpty().WithMessage("DischargeDate is required");
            RuleFor(x => x.DischargeTime).NotNull().NotEmpty().WithMessage("DischargeTime is required");


        }
    }
    public class AdmissionModel
    {
        public long AdmissionId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public byte? IsDischarged { get; set; }
    }
    public class AdmissionModelValidator : AbstractValidator<AdmissionModel>
    {
        public AdmissionModelValidator()
        {
            RuleFor(x => x.DischargeDate).NotNull().NotEmpty().WithMessage("DischargeDate is required");
            RuleFor(x => x.DischargeTime).NotNull().NotEmpty().WithMessage("DischargeTime is required");
        }
    }
    public class BedMasterModel1
    {
        public long BedId { get; set; }
    }
    public class BedMasterModel1Validator : AbstractValidator<BedMasterModel1>
    {
        public BedMasterModel1Validator()
        {
            RuleFor(x => x.BedId).NotNull().NotEmpty().WithMessage("BedId is required");

        }
    }

    public class DischargeModels
    { 
        public DischargeModel Discharge { get; set; }
        public AdmissionModel Admission { get; set; }
        public BedMasterModel1 Bed {  get; set; }

    }
    public class DischargeUpdateModel
    {
        public long DischargeId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public long? DischargeTypeId { get; set; }
        public long? DischargedDocId { get; set; }
        public long? DischargedRmoid { get; set; }
        public long? AddedBy { get; set; }
        public long? ModeOfDischargeId {  get; set; }
        public long? ModifiedBy { get;set; }


    }
    public class DischargeUpdateModelValidator : AbstractValidator<DischargeUpdateModel>
    {
        public DischargeUpdateModelValidator()
        {
            RuleFor(x => x.DischargeDate).NotNull().NotEmpty().WithMessage("DischargeDate is required");
            RuleFor(x => x.DischargeTime).NotNull().NotEmpty().WithMessage("DischargeTime is required");
        }
    }
    public class AdmissionUpdateModel
    {
        public long AdmissionId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public byte? IsDischarged { get; set; }
    }
    public class DischargUpdate
    {
        public DischargeUpdateModel Discharge { get; set; }
        public AdmissionUpdateModel Admission { get; set; }


    }
}

