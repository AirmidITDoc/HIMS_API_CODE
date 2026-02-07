using FluentValidation;


namespace HIMS.API.Models.Inventory
{
    public class IssueToDIndentModel
    {
        public long IssueId { get; set; }
        //public long? IssueNo { get; set; }
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
        public List<IssueToDepartmentDetailModel> TIssueToDepartmentDetails { get; set; }

    }
    public class IssueToDIndentModelValidator : AbstractValidator<IssueToDIndentModel>
    {
        public IssueToDIndentModelValidator()
        {
            RuleFor(x => x.IssueDate).NotNull().NotEmpty().WithMessage("IssueDate Date is required");
            RuleFor(x => x.IssueTime).NotNull().NotEmpty().WithMessage("IssueTime Time is required");
            RuleFor(x => x.FromStoreId).NotNull().NotEmpty().WithMessage("From StoreId is required");
            RuleFor(x => x.ToStoreId).NotNull().NotEmpty().WithMessage("To StoreId is required");

        }
    }
    public class IssueToDepartmentDetailModel1
    {
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
        public decimal? MRPTotalAmount { get; set; }
        public decimal? UnitPurRate { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public long? StkId { get; set; }
        public string? Status { get; set; }
    }
    public class IssueToDepartmentDetailModel1Validator : AbstractValidator<IssueToDepartmentDetailModel1>
    {
        public IssueToDepartmentDetailModel1Validator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
            RuleFor(x => x.BatchNo).NotNull().NotEmpty().WithMessage("BatchNo  is required");
            RuleFor(x => x.BatchExpDate).NotNull().NotEmpty().WithMessage("BatchExpDate  is required");

        }
    }
    public class IssueToDepVerifyModel
    {
        public long IssueId { get; set; }

    }
    public class IssuetoDeptMaterialAcceptModel
    {
        public long IssueId { get; set; }
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
        //public List<IssueToDepartmentDetailModel> TIssueToDepartmentDetails { get; set; }

    }
    public class CurrentStockMaterialModel
    {
        public int ItemId { get; set; }
        public float? IssueQty { get; set; }
        public long? IstkId { get; set; }
        public long? StoreId { get; set; }
    }
    public class CurrentStockMaterialModelValidator : AbstractValidator<CurrentStockMaterialModel>
    {
        public CurrentStockMaterialModelValidator()
        {
            RuleFor(x => x.IssueQty).NotNull().NotEmpty().WithMessage("IssueQty  is required");
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreID  is required");
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreID  is required");

        }
    }
    public class IssuetoDeptWihMaterialAcceptagainstIndentModel
    {
        public IssuetoDeptMaterialAcceptModel IssuetoDeptWihMaterialAccept { get; set; }
        public List<IssueToDepartmentDetailModel> TIssueToDepartmentDetails { get; set; }
        public List<CurrentStockMaterialModel> TCurrentStock { get; set; }
        public UpdateMaterialAcceptance materialAcceptIssueHeader { get; set; }
        public List<AcceptMaterialIssueDet> materialAcceptIssueDetails { get; set; }
        public materialAcceptStockUpdate materialAcceptStockUpdate { get; set; }
        public IndentHeaderModel IndentHeader { get; set; }
        public List<IndentDetailsModel> TIndentDetails { get; set; }

    }


    public class IssuetoDeptWihMaterialAcceptModel
    {
        public IssuetoDeptMaterialAcceptModel IssuetoDeptWihMaterialAccept { get; set; }
        public List<IssueToDepartmentDetailModel> TIssueToDepartmentDetails { get; set; }
        public List<CurrentStockMaterialModel> TCurrentStock { get; set; }
        public UpdateMaterialAcceptance materialAcceptIssueHeader { get; set; }
        public List<AcceptMaterialIssueDet> materialAcceptIssueDetails { get; set; }
        public materialAcceptStockUpdate materialAcceptStockUpdate { get; set; }
        //public IndentHeaderModel IndentHeader { get; set; }
        //public List<IndentDetailsModel> TIndentDetails { get; set; }

    }
}
