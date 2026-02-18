using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ItemDrugTypeMasterModel
    {
        public long ItemDrugTypeId { get; set; }
        public string? DrugTypeName { get; set; }
       
    }
    public class ItemDrugTypeMasterModelValidator : AbstractValidator<ItemDrugTypeMasterModel>
    {
        public ItemDrugTypeMasterModelValidator()
        {
            RuleFor(x => x.DrugTypeName).NotNull().NotEmpty().WithMessage("DrugTypeName  is required");
        }
    }
}
