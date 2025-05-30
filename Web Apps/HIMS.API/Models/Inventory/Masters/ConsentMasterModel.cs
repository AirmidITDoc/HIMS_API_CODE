using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class ConsentMasterModel
    {

        public long ConsentId { get; set; }
        public long? DepartmentId { get; set; }
        public string? ConsentName { get; set; }
        public string? ConsentDesc { get; set; }
       

    }
    public class ConsentMasterModelValidator : AbstractValidator<ConsentMasterModel>
    {
        public ConsentMasterModelValidator()
        {
            RuleFor(x => x.ConsentDesc).NotNull().NotEmpty().WithMessage("DepartmentId is required");
            RuleFor(x => x.ConsentName).NotNull().NotEmpty().WithMessage("ConsentName is required");
            RuleFor(x => x.ConsentDesc).NotNull().NotEmpty().WithMessage("ConsentDesc is required");


        }
    }
}
