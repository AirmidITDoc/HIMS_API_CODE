using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory
{
    public class SupplierModel
    {
        public long SupplierId { get; set; }
        public string? SupplierName { get; set; }
        

    }

    public class SupplierModelValidator : AbstractValidator<SupplierModel>
    {
        public SupplierModelValidator()
        {
            RuleFor(x => x.SupplierName).NotNull().NotEmpty().WithMessage("Supplier  Name is required");
        }
    }


}


