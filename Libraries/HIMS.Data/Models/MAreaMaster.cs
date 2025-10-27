namespace HIMS.Data.Models
{
    public partial class MAreaMaster
    {
        public long AreaId { get; set; }
        public string? AreaName { get; set; }
        public long? TalukaId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CityId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
