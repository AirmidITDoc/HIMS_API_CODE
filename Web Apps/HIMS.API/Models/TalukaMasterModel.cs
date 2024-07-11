using FluentValidation;

namespace HIMS.API.Models
{
    public class TalukaMasterModel
    {
        public long TalukaId { get; set; }
        public string? TalukaName { get; set; }
        public long? CityId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class TalukaMasterModelValidator : AbstractValidator<TalukaMasterModel>
    {
        public TalukaMasterModelValidator()
        {
            RuleFor(x => x.TalukaName).NotNull().NotEmpty().WithMessage("TalukaName is required");
        }
    }
}
