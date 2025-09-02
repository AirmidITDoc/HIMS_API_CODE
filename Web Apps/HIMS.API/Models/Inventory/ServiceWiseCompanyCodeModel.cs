using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Inventory
{
    public class ServiceWiseCompanyCodeModel
    {
       public long TariffId {  get; set; }
    }
}
public class ServiceWiseCompanyCodeModelValidator : AbstractValidator<ServiceWiseCompanyCodeModel>
{
    public ServiceWiseCompanyCodeModelValidator()
    {
        RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
     
    }
}

