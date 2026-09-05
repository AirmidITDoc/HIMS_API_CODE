using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class ProcurementDashboard
    {
        public POModel POModel { get; set; }
        public IndentModel IndentModel { get; set; }
        public GRNModel GRNModel { get; set; }
        public PurchaseModel PurchaseModel { get; set; }
        public SupplierModel SupplierModel { get; set; }
        public ItemModel ItemModel { get; set; }
        public List<TrendModel> TrendModel { get; set; }
        public List<PurchaseTrendModel> PurchaseTrendModel { get; set; }
        public List<IndentTrendModel> IndentTrendModel { get; set; }
    }
    public class POModel
    {
        public long POClosedToday { get; set; }
        public long POOpenToday { get; set; }
        public decimal POClosedDiff { get; set; }
        public decimal POOpenDiff { get; set; }
    }

    public class IndentModel
    {
        public long IndentIssuedToday { get; set; }
        public long IndentClosedToday { get; set; }
        public long IndentPendingToday { get; set; }

        public decimal IndentIssuedDiff { get; set; }
        public decimal IndentClosedDiff { get; set; }
        public decimal IndentPendingDiff { get; set; }
    }

    public class GRNModel
    {
        public long GRNCountToday { get; set; }
        public long GRNApprovalPendingToday { get; set; }
        public decimal GRNValueToday { get; set; }

        public decimal GRNCountDiff { get; set; }
        public decimal GRNApprovalPendingDiff { get; set; }
        public decimal GRNValueDiff { get; set; }
    }

    public class PurchaseModel
    {
        public long PurchaseReturnCount { get; set; }
        public long PurchaseReturnPendingApprovalCount { get; set; }
        public decimal PurchaseReturnValue { get; set; }
        public long WithoutPOGRNCount { get; set; }
        public long VendorPaymentDueCount { get; set; }
        public long RCCount { get; set; }
    }

    public class SupplierModel
    {
        public string SupplierName { get; set; }
        public long PurchaseCount { get; set; }
        public decimal PurchaseValue { get; set; }
        public long PurchaseReturnCount { get; set; }
        public decimal PurchaseReturnValue { get; set; }
    }

    public class ItemModel
    {
        public string ItemName { get; set; }
        public long PurchaseQty { get; set; }
        public decimal PurchaseValue { get; set; }
    }

    public class TrendModel
    {
        public DateTime TrendDate { get; set; }
        public long POCount { get; set; }
        public long GRNCount { get; set; }
    }

    public class PurchaseTrendModel
    {
        public DateTime TrendDate { get; set; }
        public decimal PurchaseValue { get; set; }
        public decimal PurchaseReturnValue { get; set; }
    }

    public class IndentTrendModel
    {
        public DateTime TrendDate { get; set; }
        public long IndentIssued { get; set; }
        public long IndentClosed { get; set; }
        public long IndentPending { get; set; }
    }
}
