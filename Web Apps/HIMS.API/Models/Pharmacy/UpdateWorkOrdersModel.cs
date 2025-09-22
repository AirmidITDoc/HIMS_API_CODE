using FluentValidation;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class UpdateWorkOrdersModel
    {
        public long Woid { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierID { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? DiscAmount { get; set; }
        public double? VatAmount { get; set; }

        public double? NetAmount { get; set; }
        public bool? Isclosed { get; set; }
        public string? Remark { get; set; }
        public long? UpdatedBy { get; set; }
      
    }
    public class UpdateWorkOrdersModelValidator : AbstractValidator<UpdateWorkOrdersModel>
    {
        public UpdateWorkOrdersModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
            RuleFor(x => x.SupplierID).NotNull().NotEmpty().WithMessage("SupplierID is required");

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
        public double? VatAmount { get; set; }
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
        //public List<BillingDetailsModel>? BillingDetails { get; set; }
        //public paymentsModel? payments { get; set; }
        //public paymentModel? payment { get; set; }
        //public BillMModel? Bills { get; set; }
        //public List<AdvancesDetailModel?> Advancesupdate { get; set; }
        //public AdvancesHeaderModel? advancesHeaderupdate { get; set; }
        //public AddChargessModel? AddChargessupdate { get; set; }
    }
}