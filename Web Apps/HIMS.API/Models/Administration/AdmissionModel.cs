using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Administration
{
    public class AdmissionsModel
    {
        public long AdmissionID { get; set; }
     

    }

    public class AdmissionsModelValidator : AbstractValidator<AdmissionsModel>
    {
        public AdmissionsModelValidator()
        {
         //   RuleFor(x => x.IsCancelledBy).NotNull().NotEmpty().WithMessage("IsCancelledBy id is required");
            //  RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
        }
    }
}