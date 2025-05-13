using FluentValidation;
using HIMS.API.Models.Pharmacy;

namespace HIMS.API.Models.Inventory
{
    public class IndentModel
    {
        public long IndentId { get; set; }
        public string? IndentDate { get; set; }
        public string? IndentTime { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public long? Isdeleted { get; set; }
        public bool? Isverify { get; set; }
        public bool? Isclosed{ get; set; }
        public string? Comments { get; set; }
        public List<IndentDetailModel> TIndentDetails { get; set; }
    }
    public class IndentModelValidator : AbstractValidator<IndentModel>
    {
        public IndentModelValidator()
        {
            RuleFor(x => x.IndentDate).NotNull().NotEmpty().WithMessage("Indent Date is required");
            RuleFor(x => x.IndentTime).NotNull().NotEmpty().WithMessage("Indent Time is required");
            RuleFor(x => x.FromStoreId).NotNull().NotEmpty().WithMessage("From StoreId is required");
            RuleFor(x => x.ToStoreId).NotNull().NotEmpty().WithMessage("To StoreId is required");

        }
    }
    public class IndentDetailModel
    {
        public long IndentId { get; set; }
        public long ItemId { get; set; }
        public long Qty { get; set; }
        public bool? Isclosed { get; set; }
        public long? IndQty { get; set; }
        public long? IssQty { get; set; }

    }
    public class IndentDetailModelValidator : AbstractValidator<IndentDetailModel>
    {
        public IndentDetailModelValidator()
        {
            RuleFor(x => x.IndentId).NotNull().NotEmpty().WithMessage("Indent Id is required");
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("Item is required");
            RuleFor(x => x.Qty).NotNull().NotEmpty().WithMessage("Qty is required");
        }
    }

    public class IndentVerifyModel
    {
        public long IndentId { get; set; }
        public long IsInchargeVerifyId { get; set; }
    }
    public class IndentVerifyModelValidator : AbstractValidator<IndentVerifyModel>
    {
        public IndentVerifyModelValidator()
        {
            RuleFor(x => x.IndentId).NotNull().NotEmpty().WithMessage("Indent id is required");
            RuleFor(x => x.IsInchargeVerifyId).NotNull().NotEmpty().WithMessage("Incharge verified id is required");
        }
    }

    public class IndentCancel
    {
        public long IndentId { get; set; }
    }
}
