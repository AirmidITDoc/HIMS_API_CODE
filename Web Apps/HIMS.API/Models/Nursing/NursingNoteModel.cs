using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Nursing
{
    public class NursingNoteModel
    {
        public long DocNoteId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Tdate { get; set; }
        public string? Ttime { get; set; }
        public string? NursingNotes { get; set; }
    }
    public class NursingNoteModelValidator : AbstractValidator<NursingNoteModel>
    {
        public NursingNoteModelValidator()
        {
            RuleFor(x => x.AdmId).NotNull().NotEmpty().WithMessage("AdmId  is required");
            RuleFor(x => x.Tdate).NotNull().NotEmpty().WithMessage("Tdate  is required");
            RuleFor(x => x.Ttime).NotNull().NotEmpty().WithMessage("Ttime  is required");

        }
    }
}
