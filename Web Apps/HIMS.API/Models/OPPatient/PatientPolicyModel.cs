using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.OPPatient
{
    public class PatientPolicyModel
    {
        public long? PatientPolicyId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? PolicyNo { get; set; }
        public DateTime? PolicyValidateDate { get; set; }
        public decimal? ApprovedAmount { get; set; }
    }
    public class PatientPolicyModelValidator : AbstractValidator<PatientPolicyModel>
    {
        public PatientPolicyModelValidator()
        {
            RuleFor(x => x.Opipid).NotNull().NotEmpty().WithMessage("Opipid is required");
        }
    }
}
