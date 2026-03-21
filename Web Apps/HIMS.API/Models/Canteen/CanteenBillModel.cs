using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Canteen
{
    public class CanteenBillModel
    {
        public long BillNo { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? StoreId { get; set; }
        public long? OpIpId { get; set; }
        public string? CustomerName { get; set; }
        public string? PbillNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public float? Gstper { get; set; }
        public decimal? Gstamount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public long? ConcessionReasonId { get; set; }
        public long? ConcessionAuthorizationId { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsPrint { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? ReqId { get; set; }
        public bool? IsOtherOrIsEmpBill { get; set; }
        public List<CanteenBillDetailModel> TCanteenBillDetails { get; set; }


    }
    public class CanteenBillDetailModel
    {
        public long CdetId { get; set; }
        public long? BillNo { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? UnitMrp { get; set; }
        public double? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? Gstper { get; set; }
        public decimal? Gstamount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? LandedPrice { get; set; }
        public decimal? TotalLandedAmount { get; set; }
        public double? ReturnQty { get; set; }
        public long? ReqId { get; set; }
        public long? ReqDetId { get; set; }
    }
}
