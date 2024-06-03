namespace HIMS.Core.Domain.Grid
{
    public class ListRequestModel
    {
        public DateTime? From_Dt { get; set; }
        public DateTime? To_Dt { get; set; }
        public long? Supplier_Id { get; set; }
        public long? ToStoreId { get; set; }
        public string? IsVerify { get; set; }
        public int? Length { get; set; }
        public int? Start { get; set; }
    }

}