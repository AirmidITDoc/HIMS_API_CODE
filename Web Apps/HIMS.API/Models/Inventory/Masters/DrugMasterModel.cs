using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DrugMasterModel
    {
        public long DrugId { get; set; }
        public string? DrugName { get; set; }
        public long? GenericId { get; set; }
        public long? ClassId { get; set; }
    }
    public class DrugMasterModelValidator : AbstractValidator<DrugMasterModel>
    {
        public DrugMasterModelValidator()
        {
            RuleFor(x => x.DrugName).NotNull().NotEmpty().WithMessage("DrugName is required");
            RuleFor(x => x.GenericId).NotNull().NotEmpty().WithMessage("GenericId is required");
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");

        }
    }
}
