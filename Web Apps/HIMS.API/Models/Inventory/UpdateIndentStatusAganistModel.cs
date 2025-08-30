using DocumentFormat.OpenXml.Bibliography;
using FluentValidation;
using HIMS.API.Models.Pharmacy;

namespace HIMS.API.Models.Inventory
{
    public class UpdateIndentStatusAganistModel
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
        public long IssueId { get; set; }
        public List<IssueToDepartmentDetailsUpdateModel> TIssueToDepartmentDetails { get; set; }


    }
    public class UpdateIndentStatusAganistModelValidator : AbstractValidator<UpdateIndentStatusAganistModel>
    {
        public UpdateIndentStatusAganistModelValidator()
        {
            RuleFor(x => x.IssueDate).NotNull().NotEmpty().WithMessage("IssueDate  is required");
            RuleFor(x => x.IssueTime).NotNull().NotEmpty().WithMessage("IssueTime  is required");
            RuleFor(x => x.FromStoreId).NotNull().NotEmpty().WithMessage("FromStoreId  is required");
            RuleFor(x => x.ToStoreId).NotNull().NotEmpty().WithMessage("ToStoreId  is required");
            RuleFor(x => x.TotalAmount).NotNull().NotEmpty().WithMessage("TotalAmount  is required");
            RuleFor(x => x.TotalVatAmount).NotNull().NotEmpty().WithMessage("TotalVatAmount  is required");


        }
    }
    public class IssueToDepartmentDetailsUpdateModel
    {
        public long IssueId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? IssueQty { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public decimal? UnitPurRate { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public double? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public long? StkId { get; set; }
    }
    public class TCurrentStockModel
    {
        public long? ItemId { get; set; }
        public float? IssueQty { get; set; }
        public long? IstkId { get; set; }
        public long? StoreId { get; set; }

    }
    public class IndentHeaderModel
    {
        public long IndentId { get; set; }
        public bool? IsClosed { get; set; }
       
    }
    public class IndentDetailsModel
    {
        public long IndentId { get; set; }
        public long IndentDetailsId { get; set; }
        public bool? IsClosed { get; set; }
        public long? IndQty { get; set; }


    }

    public class UpdateIndentStatusModel
    {
        public UpdateIndentStatusAganistModel UpdateIndent { get; set; }
        public List<TCurrentStockModel> TCurStockModel { get; set; }
        public IndentHeaderModel IndentHeader { get; set; }
        public List<IndentDetailsModel> TIndentDetails { get; set; }

    }
    public class UpdateMaterialAcceptance
    {
        public long IssueId { get; set; }
        public long? AcceptedBy { get; set; }
        public bool? IsAccepted { get; set; }
    }
    public class AcceptMaterialIssueDet
    {
        public long IssueId { get; set; }
        public long IssueDepId { get; set; }
        public string? Status { get; set; }
    }
    public class materialAcceptStockUpdate
    {
        public long IssueId { get; set; }
    }


    public class UpdateMaterialAcceptanceModel
    {
        public UpdateMaterialAcceptance materialAcceptIssueHeader { get; set; }
        public List<AcceptMaterialIssueDet> materialAcceptIssueDetails { get; set; }
        public materialAcceptStockUpdate materialAcceptStockUpdate { get; set; }

    }
}

