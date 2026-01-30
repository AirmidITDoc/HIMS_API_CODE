using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class IssueToDepartmentModel
    {
        public DateTime? IssueDate { get; set; }
        public string? IssueTime { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public double? TotalAmount { get; set; }
        public double? TotalVatAmount { get; set; }
        public double? NetAmount { get; set; }
        public string? Remark { get; set; }
        public long? Addedby { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsClosed { get; set; }
        public long? IndentId { get; set; }
        public long? UnitId { get; set; }
        public long? CreatedBy { get; set; }
        public long IssueId { get; set; }
        public List<IssueToDepartmentDetailModel> TIssueToDepartmentDetails { get; set; }
    }
    public class IssueToDepartmentModelValidator : AbstractValidator<IssueToDepartmentModel>
    {
        public IssueToDepartmentModelValidator()
        {
            RuleFor(x => x.IssueDate).NotNull().NotEmpty().WithMessage("IssueDate Date is required");
            RuleFor(x => x.IssueTime).NotNull().NotEmpty().WithMessage("IssueTime Time is required");

        }
    }
    public class IssueToDepartmentDetailModel
    {
        public long IssueDepId { get; set; }
        public long? IssueId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? IssueQty { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public double? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public decimal? UnitPurRate { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public long? StkId { get; set; }
        public string? Status { get; set; }
    }
    public class IssueToDepartmentDetailModelValidator : AbstractValidator<IssueToDepartmentDetailModel>
    {
        public IssueToDepartmentDetailModelValidator()
        {
            RuleFor(x => x.BatchExpDate).NotNull().NotEmpty().WithMessage("BatchExpDate  is required");

        }
    }
    
    public class CurrentStockModel
    {
        public int ItemId { get; set; }
        public int IssueQty { get; set; }
        public int IStkId { get; set; }
        public int StoreID { get; set; }
    }
    public class CurrentStockModelValidator : AbstractValidator<CurrentStockModel>
    {
        public CurrentStockModelValidator()
        {
            RuleFor(x => x.IssueQty).NotNull().NotEmpty().WithMessage("IssueQty  is required");
            RuleFor(x => x.IStkId).NotNull().NotEmpty().WithMessage("IStkId  is required");
            RuleFor(x => x.StoreID).NotNull().NotEmpty().WithMessage("StoreID  is required");

        }
    }
    public class IssueTODepModel
    {
        public IssueToDepartmentModel issue { get; set; }

        //   public IssueToDepartmentDetailModel Depissue { get; set; }
        public List<CurrentStockModel> TCurrentStock { get; set; }
    }

}
