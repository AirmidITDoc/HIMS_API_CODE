using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class IPDischargeModel
    {
        public long DischargeId { get; set; }
        public long? AdmissionId { get; set; }
        public string? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public long? DischargeTypeId { get; set; }
        public long? DischargedDocId { get; set; }
        public long? DischargedRmoid { get; set; }
        public long? AddedBy { get; set; }
        
    }

    public class IPDischargeModelValidator : AbstractValidator<IPDischargeModel>
    {
        public IPDischargeModelValidator()
        {
            RuleFor(x => x.DischargeDate).NotNull().NotEmpty().WithMessage("DischargeDate is required");
            RuleFor(x => x.DischargeTime).NotNull().NotEmpty().WithMessage("DischargeTime is required");


        }
    }

    public class DischargeADMISSIONModel
    {
        public long AdmissionID { get; set; }
     
        public string? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public long? IsDischarged { get; set; }
     
    }
    public class DischargeADMISSIONModelValidator : AbstractValidator<DischargeADMISSIONModel>
    {
        public DischargeADMISSIONModelValidator()
        {
            //RuleFor(x => x.DischargeDate).NotNull().NotEmpty().WithMessage("DischargeDate is required");
            RuleFor(x => x.DischargeTime).NotNull().NotEmpty().WithMessage("DischargeTime is required");


        }
    }

    public class BedReleaseModel
    {
        public long BedId { get; set; }
    }

    public class NewDischarge
    {
        public IPDischargeModel DischargeModel { get; set; }
        public DischargeADMISSIONModel DischargeAdmissionModel { get; set; }
        public BedReleaseModel BedModel { get; set; }
    }

}
