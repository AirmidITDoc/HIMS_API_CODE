using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class ConsentInformationModel
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
        public int? CreatedBy { get; set; }

    }

    public class ConsentInformationModelValidator : AbstractValidator<ConsentInformationModel>
    {
        public ConsentInformationModelValidator()
        {
            RuleFor(x => x.ConsentName).NotNull().NotEmpty().WithMessage("StoreId is required");

        }
    }
}