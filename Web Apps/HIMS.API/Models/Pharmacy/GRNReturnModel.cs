using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class GRNReturnModel
    {
        public long? Grnid { get; set; }
        public DateTime? GrnreturnDate { get; set; }
        public string GrnreturnTime { get; set; }
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
        public long? AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public bool? IsClosed { get; set; }
        public string? GrnType { get; set; }
        public bool IsGrnTypeFlag { get; set; }
        public long GrnreturnId { get; set; }
        public long UnitId { get; set; }

        //public List<GRNReturnDetailModel> TGrnreturnDetails { get; set; }
    }
    public class GRNReturnModelValidator : AbstractValidator<GRNReturnModel>
    {
        public GRNReturnModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("Store is required");
            RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("Supplier is required");
            RuleFor(x => x.GrnreturnDate).NotNull().NotEmpty().WithMessage("GrnreturnDate  is required");
            RuleFor(x => x.GrnreturnTime).NotNull().NotEmpty().WithMessage("GrnreturnTime  is required");

        }
    }

    public class GRNReturnReqDto
    {
        public GRNReturnModel GrnReturn { get; set; }
        public List<GRNReturnDetailModel> tGrnreturnDetails { get; set; }
        public List<GRNReturnCurrentStock> GrnReturnCurrentStock { get; set; }
        public List<GRNReturnReturnQty> GrnReturnReturnQt { get; set; }
    }
}
