using FluentValidation;

namespace HIMS.API.Models.OTManagement
{
    public class OTAnaesthesiaClearanceModel
    {
        public long AnaesthesiaClearanceId { get; set; }
        public long Opipid { get; set; }
        public byte Opiptype { get; set; }
        public long AnesthetistId { get; set; }
        public long? Asagrade { get; set; }
        public long? Airway { get; set; }
        public long AnaesthesiaTypeId { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Remarks { get; set; }
        public long ClearanceStatus { get; set; }
        public DateTime ClearanceDate { get; set; }


    }
    public class OTAnaesthesiaClearanceModelValidator : AbstractValidator<OTAnaesthesiaClearanceModel>
    {
        public OTAnaesthesiaClearanceModelValidator()
        {
            
            RuleFor(x => x.Opipid).NotNull().NotEmpty().WithMessage("Opipid is required");
            RuleFor(x => x.AnesthetistId).NotNull().NotEmpty().WithMessage("AnesthetistId is required");
            RuleFor(x => x.AnaesthesiaTypeId).NotNull().NotEmpty().WithMessage("AnaesthesiaTypeId is required");
            RuleFor(x => x.ClearanceStatus).NotNull().NotEmpty().WithMessage("ClearanceStatus is required");
            RuleFor(x => x.ClearanceDate).NotNull().NotEmpty().WithMessage("ClearanceDate is required");

        }
    }
}
