using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class GRNUpdatedModel
    {
        public long GrnreturnId { get; set; }
        public long? Grnid { get; set; }
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
        public bool? IsClosed { get; set; }
        public bool IsCancelled { get; set; }
        public long? AddedBy { get; set; }
        public string? GrnType { get; set; }
        public bool IsGrnTypeFlag { get; set; }


    }

    public class GRNReturnUpdatereqDto
    {
        public GRNUpdatedModel GrnReturn { get; set; }
        //public List<GRNReturnCurrentStock> GrnReturnCurrentStock { get; set; }
        //public List<GRNReturnReturnQty> GrnReturnReturnQt { get; set; }
    }
}