using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{

    public class ItemModel
    {
        public long ItemId { get; set; }
        public string? Hsncode { get; set; }
        public double? Cgst { get; set; }
        public double? Sgst { get; set; }
        public double? Igst { get; set; }
    }

    public class POHeaderAganistGRNModel
    {
        public long PurchaseId { get; set; }
        public long? PurDetId { get; set; }
        public double? PobalQty { get; set; }
        public bool? IsClosed { get; set; }
    }

    public class POAganistGRNModel
    {
        public long PurchaseId { get; set; }
        public bool? IsClosed { get; set; }

    }

    public class GRNReturnCurrentStock
    {
        public long? ItemId { get; set; }
        public float? IssueQty { get; set; }
        public long? IStkId { get; set; }
        public long? StoreId { get; set; }
    }

    public class GRNReturnReturnQty
    {
        public long GrndetId { get; set; }
        public float? ReturnQty { get; set; }

    }
    public class GRNVerifyModel
    {
        public long? Grnid { get; set; }
        public long VerifiedBy { get; set; }
    }
    public class GRNVerifyModelValidator : AbstractValidator<GRNVerifyModel>
    {
        public GRNVerifyModelValidator()
        {
            RuleFor(x => x.Grnid).NotNull().NotEmpty().WithMessage("Grn id is required");
         //   RuleFor(x => x.IsVerifiedUserId).NotNull().NotEmpty().WithMessage("Verified user id is required");
        }
    }

    public class GRNReturnVerifyModel
    {
        public long? GrnreturnId { get; set; }
        public long IsVerified { get; set; }
    }
    public class GRNReturnVerifyModelValidator : AbstractValidator<GRNReturnVerifyModel>
    {
        public GRNReturnVerifyModelValidator()
        {
            RuleFor(x => x.GrnreturnId).NotNull().NotEmpty().WithMessage("Grn id is required");
            //RuleFor(x => x.IsVerifiedUserId).NotNull().NotEmpty().WithMessage("Verified user id is required");
        }
    }
}