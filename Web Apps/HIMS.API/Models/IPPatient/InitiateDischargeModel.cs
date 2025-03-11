using FluentValidation;

namespace HIMS.API.Models.IPPatient
{
    public class InitiateDischargeModel
    {
        public long InitateDiscId { get; set; }
        public long? AdmId { get; set; }
        public string? DepartmentName { get; set; }
        public long? DepartmentId { get; set; }
        public bool? IsApproved { get; set; }
        public long? ApprovedBy { get; set; }
        public DateTime? ApprovedDatetime { get; set; }
        public bool? IsNoDues { get; set; }
        public string? Comments { get; set; }
    }
    public class InitiateDischargeModelValidator : AbstractValidator<InitiateDischargeModel>
    {
        public InitiateDischargeModelValidator()
        {
            RuleFor(x => x.ApprovedDatetime).NotNull().NotEmpty().WithMessage("ApprovedDatetime is required");
            RuleFor(x => x.DepartmentName).NotNull().NotEmpty().WithMessage("DepartmentName is required");
        }
    }
    public class AdmisionModel
    {
        public long AdmID {  get; set; }     
        public bool IsInitinatedDischarge {  get; set; }
    }
    public class AdmisionModelValidator : AbstractValidator<AdmisionModel>
    {
        public AdmisionModelValidator()
        {
            RuleFor(x => x.AdmID).NotNull().NotEmpty().WithMessage("AdmID is required");
            RuleFor(x => x.IsInitinatedDischarge).NotNull().NotEmpty().WithMessage("IsInitinatedDischarge is required");
        }
    }
    public class InitiateDisModel 
    {
        public long? AdmId { get; set; }
        public string? DepartmentName { get; set; }
        public long? DepartmentId { get; set; }
        public bool? IsApproved { get; set; }
        public long? ApprovedBy { get; set; }
        public DateTime? ApprovedDatetime { get; set; }
        
    }
    public class InitiateModelValidator : AbstractValidator<InitiateDisModel>
    {
        public InitiateModelValidator()
        {
            RuleFor(x => x.ApprovedDatetime).NotNull().NotEmpty().WithMessage("ApprovedDatetime is required");
            RuleFor(x => x.DepartmentName).NotNull().NotEmpty().WithMessage("DepartmentName is required");
        }
    }
    public class InitiateDModel
    {
        public InitiateDischargeModel InitiateDischarge { get; set; }
        public AdmisionModel Admision { get; set; }


    }

}
