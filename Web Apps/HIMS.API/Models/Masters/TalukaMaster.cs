using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public interface TalukaMasterModel
    {
        
            public long TalukaId { get; set; }
            public string? TalukaName { get; set; }
            public long? CityId { get; set; }
            public bool? IsDeleted { get; set; }
            public long? AddedBy { get; set; }
            public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }
        public long? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public long? ModifiedDate { get; set; }

    }
        public class TalukaMasterModelValidator : AbstractValidator<TalukaMasterModel>
        {
            public TalukaMasterModelValidator()
            {
                RuleFor(x => x.TalukaName).NotNull().NotEmpty().WithMessage("Taluka is required");
            }
        }
    }

