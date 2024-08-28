using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class MaritalStatusModel
    {
        public long MaritalStatusId { get; set; }
        public string? MaritalStatusName { get; set; }
        
        
    }
    public class MaritalStatusModelValidator : AbstractValidator<MaritalStatusModel>
    {
        public MaritalStatusModelValidator()
        {
            RuleFor(x => x.MaritalStatusName).NotNull().NotEmpty().WithMessage("Marital Status is required");
           

        }
    }
}
