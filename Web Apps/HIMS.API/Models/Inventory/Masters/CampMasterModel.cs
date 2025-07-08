using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CampMasterModel
    {
        public long CampId { get; set; }
        public string? CampName { get; set; }
        public string? CampLocation { get; set; }
        

    }
    public class CampMasterModelValidator : AbstractValidator<CampMasterModel>
    {
        public CampMasterModelValidator()
        {
            RuleFor(x => x.CampName).NotNull().NotEmpty().WithMessage("Camp Name is required");
            RuleFor(x => x.CampLocation).NotNull().NotEmpty().WithMessage("CampLocation is required");
            
           
        }
    }
}

