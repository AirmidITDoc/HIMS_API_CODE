using FluentValidation;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class WorkOrdersModel
    {
        public long Woid { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierID { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? Isclosed { get; set; }
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
            RuleFor(x => x.SupplierID).NotNull().NotEmpty().WithMessage("SupplierID is required");
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
        public double? Qty { get; set; }
        public double? Rate { get; set; }
        public double? TotalAmount { get; set; }
        public double? DiscPer { get; set; }
        public double? DiscAmount { get; set; }
        public double? VatPer { get; set; }
        public double? VatAmount { get; set; }
        public double? NetAmount { get; set; }
        public string? Remark { get; set; }
     
    }
    public class WorkOrderDetailsModelValidator : AbstractValidator<WorkOrderDetailsModel>
    {
        public WorkOrderDetailsModelValidator()
        {
            RuleFor(x => x.Rate).NotNull().NotEmpty().WithMessage("Rate is required");
            RuleFor(x => x.TotalAmount).NotNull().NotEmpty().WithMessage("TotalAmount is required");
            RuleFor(x => x.DiscPer).NotNull().NotEmpty().WithMessage("DiscPer is required");
            RuleFor(x => x.VatAmount).NotNull().NotEmpty().WithMessage("VatAmount is required");
            RuleFor(x => x.DiscAmount).NotNull().NotEmpty().WithMessage("DiscAmount is required");
            RuleFor(x => x.VatAmount).NotNull().NotEmpty().WithMessage("VatAmount is required");
            RuleFor(x => x.NetAmount).NotNull().NotEmpty().WithMessage("NetAmount is required");

        }
    }
    public class WorksOrderModel
    {

        public WorkOrdersModel? WorkOrders { get; set; }
        public List<WorkOrderDetailsModel>? WorkOrderDetails { get; set; }
      
    }
}