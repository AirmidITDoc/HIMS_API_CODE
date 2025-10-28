namespace HIMS.Data.Models
{
    public partial class MDietChartDetail
    {
        public long DietChartDetId { get; set; }
        public long? DietChardId { get; set; }
        public long? DietItemId { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
