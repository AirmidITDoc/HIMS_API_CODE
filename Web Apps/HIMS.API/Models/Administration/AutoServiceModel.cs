using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Nursing;
using System.ComponentModel.DataAnnotations;

namespace HIMS.API.Models.Administration
{
    public class AutoServiceModel
    {
        public long SysId { get; set; }
        public long? ServiceId { get; set; }
        public bool? IsAutoBedCharges { get; set; }
    
    }

    public class AutoServiceModelValidator : AbstractValidator<AutoServiceModel>
    {
        public AutoServiceModelValidator()
        {
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("AdmId is required");

        }
    }

    public class MAutoServiceModel
    {
        public List<AutoServiceModel> AutoService { get; set; }


    }
}