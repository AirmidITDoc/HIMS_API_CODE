using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class TalukaMasterModel1
    {
        public long TalukaId { get; set; }
        public string? TalukaName { get; set; }

    }
    public class TalukaMasterModel1Validator : AbstractValidator<TalukaMasterModel1>
    {
        public TalukaMasterModel1Validator()
        {
            RuleFor(x => x.TalukaName).NotNull().NotEmpty().WithMessage("TalukaName  is required");
        }
    }
}
