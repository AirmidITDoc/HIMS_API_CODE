using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class GRNReturnModel
    {
        public long GrnreturnId { get; set; }
        public string? GrnreturnNo { get; set; }
        public long? Grnid { get; set; }
        public DateTime? GrnreturnDate { get; set; }
        public DateTime? GrnreturnTime { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? GrnReturnAmount { get; set; }
        public decimal? TotalDiscAmount { get; set; }
        public decimal? TotalVatAmount { get; set; }
        public decimal? TotalOtherTaxAmount { get; set; }
        public decimal? TotalOctroiAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? CashCredit { get; set; }
        public string? Remark { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsClosed { get; set; }
        public bool IsCancelled { get; set; }
        public string? GrnType { get; set; }
        public bool IsGrnTypeFlag { get; set; }
        public List<GRNReturnDetailModel> TGrnreturnDetails { get; set; }
    }
    public class GRNReturnModelValidator : AbstractValidator<GRNReturnModel>
    {
        public GRNReturnModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("Store is required");
            RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("Supplier is required");
            //RuleFor(x => x.InvoiceNo).NotNull().NotEmpty().WithMessage("Invoice No is required");
        }
    }

    public class GRNReturnReqDto
    {
        public GRNReturnModel GrnReturn { get; set; }
        public List<GRNReturnCurrentStock> GrnReturnCurrentStock { get; set; }
        public List<GRNReturnReturnQty> GrnReturnReturnQt { get; set; }
    }
}
