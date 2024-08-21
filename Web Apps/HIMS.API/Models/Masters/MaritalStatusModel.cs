using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class MaritalStatusModel
    {
        public long MaritalStatusId { get; set; }
        public string? MaritalStatusName { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class MaritalStatusModelValidator : AbstractValidator<MaritalStatusModel>
    {
        public MaritalStatusModelValidator()
        {
            RuleFor(x => x.MaritalStatusName).NotNull().NotEmpty().WithMessage("Marital Status is required");
            RuleFor(x => x.AddedBy).NotNull().NotEmpty().WithMessage("AddedBy  is required");

        }
    }
}
