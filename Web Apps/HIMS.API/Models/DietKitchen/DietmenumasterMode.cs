using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.MRD;

namespace HIMS.API.Models.DietKitchen
{
    public class DietmenumasterModel
    {
        public long DietMenuId { get; set; }
        public string DietMenuName { get; set; } = null!;
        public long MealTypeId { get; set; }
        public long DietTypeId { get; set; }
        public string? Texture { get; set; }
        public string? Calories { get; set; }
        public string? Protein { get; set; }
        public long CreatedBy { get; set; }


    }
    public class DietmenumasterModelValidator : AbstractValidator<DietmenumasterModel>
    {
        public DietmenumasterModelValidator()
        {
            RuleFor(x => x.DietMenuName).NotNull().NotEmpty().WithMessage("DietMenuName is required");
            //RuleFor(x => x.MealTypeId).NotNull().NotEmpty().WithMessage("MealTypeId is required");
            //RuleFor(x => x.DietTypeId).NotNull().NotEmpty().WithMessage("DietTypeId is required");
            //RuleFor(x => x.Texture).NotNull().NotEmpty().WithMessage("Texture is required");
            //RuleFor(x => x.Calories).NotNull().NotEmpty().WithMessage("Calories is required");
            //RuleFor(x => x.Protein).NotNull().NotEmpty().WithMessage("Protein is required");


        }
    }
    public class DietmenumasterDetailsModel

    {
        public long DietMenuId { get; set; }
        public long FoodItemId { get; set; }
        public int? Quantity { get; set; }
        public long? UnitId { get; set; }
        public int? SequenceNo { get; set; }
        public long? CreatedBy { get; set; }


    }
    public class DietmenumasterDetailsModelValidator : AbstractValidator<DietmenumasterDetailsModel>
    {
        public DietmenumasterDetailsModelValidator()
        {
            //RuleFor(x => x.Quantity).NotNull().NotEmpty().WithMessage("Quantity is required");
            //RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");


        }
    }
    public class DietmenumasterUpdateModel
    {
        public long DietMenuId { get; set; }
        public string DietMenuName { get; set; } = null!;
        public long MealTypeId { get; set; }
        public long DietTypeId { get; set; }
        public string? Texture { get; set; }
        public string? Calories { get; set; }
        public string? Protein { get; set; }
        public long? ModifiedBy { get; set; }
    }
    public class DietmenumasterUpdateModelValidator : AbstractValidator<DietmenumasterUpdateModel>
    {
        public DietmenumasterUpdateModelValidator()
        {
            //RuleFor(x => x.DietMenuName).NotNull().NotEmpty().WithMessage("DietMenuName is required");


        }
    }
    public class DietmenumasterModels
    {
        public DietmenumasterModel Dietmenumaster { get; set; }
        public List<DietmenumasterDetailsModel> DietMenuDetailMasters { get; set; }

    }
    public class DietmenumasterUpdateModels
    {
        public DietmenumasterUpdateModel Dietmenumaster { get; set; }
        public List<DietmenumasterDetailsModel> DietMenuDetailMasters { get; set; }

    }
}