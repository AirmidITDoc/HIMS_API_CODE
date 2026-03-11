using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Nursing
{
    public class MedicalRecordModel
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public int IntervalHours { get; set; }
        
    }
    public class MedicalRecordModelValidator : AbstractValidator<MedicalRecordModel>
    {
        public MedicalRecordModelValidator()
        {
            RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("Code  is required");
            RuleFor(x => x.IntervalHours).NotNull().NotEmpty().WithMessage("IntervalHours  is required");

        }
    }
}
