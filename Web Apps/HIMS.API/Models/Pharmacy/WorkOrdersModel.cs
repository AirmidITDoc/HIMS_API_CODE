using FluentValidation;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class WorkOrdersModel
    {
        public long WOId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierID { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? VatAmount { get; set; }
        public double? DiscAmount { get; set; }
        public double? NetAmount { get; set; }
        public bool? Isclosed { get; set; }
        public string? Remark { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
    //    public List<WorkOrderDetailsModel> TWorkOrderDetail { get; set; }
    }

    public class WorkOrdersModelValidator : AbstractValidator<WorkOrdersModel>
    {
        public WorkOrdersModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("Store is required");
            RuleFor(x => x.SupplierID).NotNull().NotEmpty().WithMessage("SupplierID is required");
            //RuleFor(x => x.TPurchaseDetails).NotNull().NotEmpty().WithMessage("Purchase items is required");
        }
    }
    public class WorkOrderDetailsModel
    {
        public long WOId { get; set; }
        public string? ItemName { get; set; }
        public double? Qty { get; set; }
        public double? Rate { get; set; }
    //    public long? SupplierID { get; set; }
        public double? TotalAmount { get; set; }
        public double? DiscPer { get; set; }
        public double? DiscAmount { get; set; }
        public double? VatPer { get; set; }
        public double? VatAmount { get; set; }
        public double? NetAmount { get; set; }
     //   public bool? Isclosed { get; set; }
        public string? Remark { get; set; }
     //   public long? AddedBy { get; set; }
       // public bool? IsCancelled { get; set; }
      //  public long? IsCancelledBy { get; set; }
    }


    public class WorksOrderModel
    {

        public WorkOrdersModel? WorkOrders { get; set; }
        public List<WorkOrderDetailsModel>? WorkOrderDetails { get; set; }
        //public List<BillingDetailsModel>? BillingDetails { get; set; }
        //public paymentsModel? payments { get; set; }
        //public paymentModel? payment { get; set; }
        //public BillMModel? Bills { get; set; }
        //public List<AdvancesDetailModel?> Advancesupdate { get; set; }
        //public AdvancesHeaderModel? advancesHeaderupdate { get; set; }
        //public AddChargessModel? AddChargessupdate { get; set; }
    }
}