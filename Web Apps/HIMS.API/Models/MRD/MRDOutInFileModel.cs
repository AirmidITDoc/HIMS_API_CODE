using FluentValidation;

namespace HIMS.API.Models.MRD
{
    public class MRDOutInFileModel
    {
        public long OutFileId { get; set; }
        public long Opipid { get; set; }
        public string? OutNo { get; set; }
        public long? GivenUserId { get; set; }
        public string? PersonName { get; set; }
        public string? OutDate { get; set; }
        public string? OutTime { get; set; }
        public string? OutReason { get; set; }
        public string? InNo { get; set; }
        public string? InDate { get; set; }
        public string? InTime { get; set; }
        public long? ReturnUserId { get; set; }
        public string? ReturnPersonName { get; set; }
        public string? InReason { get; set; }
    }

    public class MRDOutInFileModelValidator : AbstractValidator<MRDOutInFileModel>
    {
        public MRDOutInFileModelValidator()
        {
            RuleFor(x => x.Opipid).NotNull().NotEmpty().WithMessage("Opipid is required");
        }
    }
}
