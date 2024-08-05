using FluentValidation;

namespace HIMS.API.Models
{
    public class TalukaMasterModel
    {
        public long TalukaId { get; set; }
        public string? TalukaName { get; set; }
       
    }
    public class TalukaMasterModelValidator : AbstractValidator<TalukaMasterModel>
    {
        public TalukaMasterModelValidator()
        {
            RuleFor(x => x.TalukaName).NotNull().NotEmpty().WithMessage("TalukaName is required");
        }
    }
}
