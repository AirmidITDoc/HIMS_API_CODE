using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ApprovalHeaderModel
    {
        public long ApprovalId { get; set; }
        public string ApprovalNo { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Time { get; set; }
        public long TranId { get; set; }
        public string TransactionType { get; set; } = null!;
        public byte ApprovalStatus { get; set; }
        public long? AuthorizeBy { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public string? Comment { get; set; }
    }
    public class ApprovalHeaderModelValidator : AbstractValidator<ApprovalHeaderModel>
    {
        public ApprovalHeaderModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date is required");
        }
    }
}
