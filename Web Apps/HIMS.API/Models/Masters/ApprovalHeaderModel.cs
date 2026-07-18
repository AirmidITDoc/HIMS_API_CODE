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
            RuleFor(x => x.ApprovalNo).NotNull().NotEmpty().WithMessage("ApprovalNo is required");
            RuleFor(x => x.Time).NotNull().NotEmpty().WithMessage("Time is required");
            RuleFor(x => x.TransactionType).NotNull().NotEmpty().WithMessage("TransactionType is required");
            RuleFor(x => x.TranId).NotNull().NotEmpty().WithMessage("TranId is required");
            RuleFor(x => x.AuthorizeBy).NotNull().NotEmpty().WithMessage("AuthorizeBy is required");
            RuleFor(x => x.ApprovedDateTime).NotNull().NotEmpty().WithMessage("ApprovedDateTime is required");
        }
    }
    public class ApprovalHeaderUpdateModel
    {
        public long ApprovalId { get; set; }
        public byte ApprovalStatus { get; set; }
        public string? ApprovedDateTime { get; set; }
    }
    public class ApprovalHeaderUpdateModelValidator : AbstractValidator<ApprovalHeaderUpdateModel>
    {
        public ApprovalHeaderUpdateModelValidator()
        {
            RuleFor(x => x.ApprovalStatus).NotNull().NotEmpty().WithMessage("ApprovalStatus is required");
            RuleFor(x => x.ApprovedDateTime).NotNull().NotEmpty().WithMessage("ApprovedDateTime is required");
           
        }
    }
}
