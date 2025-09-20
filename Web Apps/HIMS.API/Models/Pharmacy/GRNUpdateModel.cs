using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.Pharmacy
{
    public class GRNUpdatedModel
    {
        public long? Grnid { get; set; }
        public DateTime? GrnreturnDate { get; set; }
        public string GrnreturnTime { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? GrnReturnAmount { get; set; }
        public decimal? TotalDiscAmount { get; set; }
        public decimal? TotalVatAmount { get; set; }
        public decimal? TotalOtherTaxAmount { get; set; }
        public decimal? TotalOctroiAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? CashCredit { get; set; }
        public string? Remark { get; set; }
        public bool? IsVerified { get; set; }
        public long? AddedBy { get; set; }
        public bool IsCancelled { get; set; }
        public bool? IsClosed { get; set; }
        public string? GrnType { get; set; }
        public bool IsGrnTypeFlag { get; set; }
        public long GrnreturnId { get; set; }
        //public List<GRNReturnDetailsModel> TGrnreturnDetails { get; set; }




    }
    public class GrnreturnUpdate
    {
        public long? GrnreturnId { get; set; }

    }
    public class GRNReturnDetailsModel
    {
        public long GrnreturnId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpiryDate { get; set; }
        public float? ReturnQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? UnitPurchaseRate { get; set; }
        public float? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? OtherTaxAmount { get; set; }
        public float? OctroiPer { get; set; }
        public decimal? OctroiAmt { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public decimal? PurchaseTotalAmount { get; set; }
        public short? Conversion { get; set; }
        public string? Remarks { get; set; }
        public long? StkId { get; set; }
        public float? Cf { get; set; }
        public float? TotalQty { get; set; }
        public long? Grnid { get; set; }
    }
    public class CurStockModels
    {

        public long? ItemId { get; set; }
        public float? IssueQty { get; set; }
        public long? StoreId { get; set; }
        public long? IstkId { get; set; }


    }
    public class UpdateGrnReturnQty
    {
        public long GrndetId { get; set; }
        public double? ReturnQty { get; set; }
    }

    public class GRNReturnUpdatereqDto
    {
        public GRNUpdatedModel GrnReturn { get; set; }
        public List<GRNReturnDetailsModel> tGrnreturnDetails { get; set; }
        public List<CurStockModels> GrnReturnCurrentStock { get; set; }
        public List<UpdateGrnReturnQty> GrnReturnReturnQt { get; set; }

        //public List<GRNReturnReturnQty> GrnReturnReturnQt { get; set; }
    }
}
    
