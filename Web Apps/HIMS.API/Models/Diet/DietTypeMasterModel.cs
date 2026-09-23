using FluentValidation;

namespace HIMS.API.Models.Diet
{
    public class DietTypeMasterModel
    {
        public long DietTypeId { get; set; }
        //public string DietCode { get; set; } = null!;
        public string DietName { get; set; } = null!;
        public string ShortName { get; set; } = null!;
        public string? Description { get; set; }
        public long? DietCategoryId { get; set; }
        public int? DefaultCalories { get; set; }
        public int? DefaultProtein { get; set; }
        public int? DefaultFluid { get; set; }
        public int? DisplayOrder { get; set; }
        public string? Remarks { get; set; }
    }
    public class DietTypeMasterModelValidator : AbstractValidator<DietTypeMasterModel>
    {
        public DietTypeMasterModelValidator()
        {
            //RuleFor(x => x.DietCode).NotNull().NotEmpty().WithMessage("DietCode  is required");
            //RuleFor(x => x.DietName).NotNull().NotEmpty().WithMessage("DietName  is required");
            //RuleFor(x => x.ShortName).NotNull().NotEmpty().WithMessage("ShortName  is required");
            //RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description  is required");
            //RuleFor(x => x.DietCategoryId).NotNull().NotEmpty().WithMessage("DietCategoryId  is required");
            //RuleFor(x => x.DefaultCalories).NotNull().NotEmpty().WithMessage("DefaultCalories  is required");


        }
    }
}
