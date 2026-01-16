using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class WorkOrdersModel
    {
        public long Woid { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool IsClosed { get; set; }
        public string? Remark { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
    }

    public class WorkOrdersModelValidator : AbstractValidator<WorkOrdersModel>
    {
        public WorkOrdersModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("Store is required");
            RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("SupplierID is required");
            RuleFor(x => x.TotalAmount).NotNull().NotEmpty().WithMessage("TotalAmount is required");
            RuleFor(x => x.VatAmount).NotNull().NotEmpty().WithMessage("VatAmount is required");
            RuleFor(x => x.DiscAmount).NotNull().NotEmpty().WithMessage("DiscAmount is required");
            RuleFor(x => x.NetAmount).NotNull().NotEmpty().WithMessage("NetAmount is required");
        }
    }
    public class WorkOrderDetailsModel
    {
        public long Woid { get; set; }
        public string? ItemName { get; set; }
        public long? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? Vatper { get; set; }
        public decimal? Vatamount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Remark { get; set; }

    }
    public class WorkOrderDetailsModelValidator : AbstractValidator<WorkOrderDetailsModel>
    {
        public WorkOrderDetailsModelValidator()
        {
            RuleFor(x => x.Rate).NotNull().NotEmpty().WithMessage("Rate is required");
            RuleFor(x => x.TotalAmount).NotNull().NotEmpty().WithMessage("TotalAmount is required");
            RuleFor(x => x.DiscPer).NotNull().NotEmpty().WithMessage("DiscPer is required");
            RuleFor(x => x.Vatamount).NotNull().NotEmpty().WithMessage("VatAmount is required");
            RuleFor(x => x.DiscAmount).NotNull().NotEmpty().WithMessage("DiscAmount is required");
            RuleFor(x => x.NetAmount).NotNull().NotEmpty().WithMessage("NetAmount is required");

        }
    }
    public class WorksOrderModel
    {

        public WorkOrdersModel? WorkOrders { get; set; }
        public List<WorkOrderDetailsModel>? WorkOrderDetails { get; set; }

    }
}