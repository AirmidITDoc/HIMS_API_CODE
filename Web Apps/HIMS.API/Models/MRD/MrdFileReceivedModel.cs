using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.MRD
{
    public class MrdFileReceivedModel
    {
        public long RmdrecordId { get; set; }
        public DateTime RecievedDate { get; set; }
        public string? RecievedTime { get; set; }
        public long UnitId { get; set; }
        public long Opipid { get; set; }
        public string Mrdno { get; set; } = null!;
        public string? Location { get; set; }
        public string? Comments { get; set; }
        public bool IsInOut { get; set; }
        public long? OutFileId { get; set; }
    }
    public class MrdFileReceivedModelValidator : AbstractValidator<MrdFileReceivedModel>
    {
        public MrdFileReceivedModelValidator()
        {
            RuleFor(x => x.RecievedDate).NotNull().NotEmpty().WithMessage("RecievedDate  is required");
            RuleFor(x => x.RecievedTime).NotNull().NotEmpty().WithMessage("RecievedTime  is required");

        }
    }
}
