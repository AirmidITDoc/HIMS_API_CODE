namespace HIMS.Data.Models
{
    public partial class RoomMaster
    {
        public long RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? RoomType { get; set; }
        public long? LocationId { get; set; }
        public bool? IsAvailible { get; set; }
        public bool? IsActive { get; set; }
        public long? ClassId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
