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
        public long UnitId { get; set; }
        public long? ReturnTypeId { get; set; }


    }
    public class GrnreturnUpdate
    {
        public long? GrnreturnId { get; set; }

    }
   

    public class GRNReturnUpdatereqDto
    {
        public GRNUpdatedModel GrnReturn { get; set; }
        public List<GRNReturnDetailModel> tGrnreturnDetails { get; set; }
        public List<GRNReturnCurrentStock> GrnReturnCurrentStock { get; set; }
        public List<GRNReturnReturnQty> GrnReturnReturnQt { get; set; }

    }
}

