using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SurgeryCategoryMasterModel
    {
        public long SurgeryCategoryId { get; set; }
        public string? SurgeryCategoryName { get; set; }
    }
    public class SurgeryCategoryMasterModelValidator : AbstractValidator<SurgeryCategoryMasterModel>
    {
        public SurgeryCategoryMasterModelValidator()
        {
            RuleFor(x => x.SurgeryCategoryName).NotNull().NotEmpty().WithMessage("SurgeryCategoryName is required");


        }
    }
}