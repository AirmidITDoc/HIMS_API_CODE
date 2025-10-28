namespace HIMS.Data.Models
{
    public partial class MDietChartMaster
    {
        public long DietChartId { get; set; }
        public string? DietChartName { get; set; }
        public string? DietChartDescription { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
