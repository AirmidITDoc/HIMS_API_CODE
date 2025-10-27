namespace HIMS.Data.DTO.Nursing
{
    public class CanteenListDto
    {
        public string ItemName { get; set; }
        public decimal? Price { get; set; }
        public bool? IsBatchRequired { get; set; }
        public long ItemID { get; set; }
    }
}
