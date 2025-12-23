using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class UpdateConsentInformationModel
    {
        public long ConsentId { get; set; }
        public DateTime? ConsentDate { get; set; }
        public string? ConsentTime { get; set; }
        public long? Opipid { get; set; }
        public long? Opiptype { get; set; }
        public long? ConsentDeptId { get; set; }
        public long? ConsentTempId { get; set; }
        public string? ConsentName { get; set; }
        public string? ConsentText { get; set; }
        public int? ModifiedBy { get; set; }

    }

    public class UpdateConsentInformationModelValidator : AbstractValidator<UpdateConsentInformationModel>
    {
        public UpdateConsentInformationModelValidator()
        {
            RuleFor(x => x.ConsentName).NotNull().NotEmpty().WithMessage("StoreId is required");

        }
    }
}