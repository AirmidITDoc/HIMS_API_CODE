using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class AreaMasterModel
    {
        public long AreaId { get; set; }
        public string? AreaName { get; set; }
        public long? TalukaId { get; set; }

    }
    public class AreaMasterModelValidator : AbstractValidator<AreaMasterModel>
    {
        public AreaMasterModelValidator()
        {
            RuleFor(x => x.AreaName).NotNull().NotEmpty().WithMessage("Area is required");
        }
    }
}

