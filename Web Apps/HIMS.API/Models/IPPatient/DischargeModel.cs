using FluentValidation;
using HIMS.API.Models.Inventory;


namespace HIMS.API.Models.IPPatient
{
    public class DischargeModel
    {
        public long DischargeId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public long? DischargeTypeId { get; set; }
        public long? DischargedDocId { get; set; }
        public long? DischargedRmoid { get; set; }

        public long? AddedBy { get; set; }
        public List<BedMasterModel1> BedMasters { get; set; }
        public List<AdmissionModel> Admission { get; set; }
      
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
      
        public DateTime? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public byte? IsDischarged { get; set; }
      

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

