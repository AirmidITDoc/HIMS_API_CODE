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
        
        public long? UpdatedBy  { get; set; }
    }

    public class IPDischargeModelValidator : AbstractValidator<IPDischargeModel>
    {
        public IPDischargeModelValidator()
        {
            RuleFor(x => x.AdmissionId).NotNull().NotEmpty().WithMessage("AdmissionId is required");
            //RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            //RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
            //RuleFor(x => x.GenderID).NotNull().NotEmpty().WithMessage("Gender is required");
            //RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("City is required");

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
            // RuleFor(x => x.RegId).NotNull().NotEmpty().WithMessage("regId is required");
            RuleFor(x => x.AdmissionID).NotNull().NotEmpty().WithMessage("AdmissionId is required");
           // RuleFor(x => x.PatientTypeId).NotNull().NotEmpty().WithMessage("PatientTypeId is required");
            //RuleFor(x => x.ConsultantDocId).NotNull().NotEmpty().WithMessage("ConsultantDocId is required");
            //RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
            //RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is required");
            //RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId is required");
            //RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
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
