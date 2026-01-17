using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class UpdateWorkOrdersModel
    {
        public long Woid { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? DiscAmount { get; set; }
        public double? VatAmount { get; set; }
        public double? NetAmount { get; set; }
        public bool IsClosed { get; set; }
        public string? Remark { get; set; }
        public long? UpdatedBy { get; set; }

    }
    public class UpdateWorkOrdersModelValidator : AbstractValidator<UpdateWorkOrdersModel>
    {
        public UpdateWorkOrdersModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
            RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("SupplierID is required");

        }
    }

    public class WorkOrderDetailModel
    {
        public long Woid { get; set; }
        public string? ItemName { get; set; }
        public double? Qty { get; set; }
        public double? Rate { get; set; }
        public double? TotalAmount { get; set; }
        public double? DiscPer { get; set; }
        public double? DiscAmount { get; set; }
        public double? VatPer { get; set; }
        public decimal? Vatamount { get; set; }
        public double? NetAmount { get; set; }
        public string? Remark { get; set; }

    }
    public class WorkOrderDetailModelValidator : AbstractValidator<WorkOrderDetailModel>
    {
        public WorkOrderDetailModelValidator()
        {
            RuleFor(x => x.ItemName).NotNull().NotEmpty().WithMessage("ItemName is required");
            RuleFor(x => x.Rate).NotNull().NotEmpty().WithMessage("Rate is required");

        }
    }


    public class UpdateWorkOrderModel
    {

        public UpdateWorkOrdersModel? WorkOrders { get; set; }
        public List<WorkOrderDetailModel>? WorkOrderDetails { get; set; }
     
    }
}