using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public interface AreaMasterModel
    {
        public long AreaId { get; set; }
        public string? AreaName { get; set; }
        public long? TalukaId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? CreatedBy { get; set; }
        public long? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long? ModifiedDate { get; set; }

    }
    public class AreaMasterModelValidator : AbstractValidator<AreaMasterModel>
    {
        public AreaMasterModelValidator()
        {
            RuleFor(x => x.AreaName).NotNull().NotEmpty().WithMessage("Area is required");
        }
    }
}

