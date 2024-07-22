using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models
{
    public class StateMasterModel
    {
        public long StateId { get; set; }
        public string? StateName { get; set; }
        public long? CountryId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class StateMasterModelValidator : AbstractValidator<StateMasterModel>
    {
        public StateMasterModelValidator()
        {
            RuleFor(x => x.StateName).NotNull().NotEmpty().WithMessage("StateName is required");
        }
    }
}
