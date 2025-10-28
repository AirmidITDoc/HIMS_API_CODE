namespace HIMS.Data.Models
{
    public partial class NotificationMaster
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string NotiTitle { get; set; } = null!;
        public string NotiBody { get; set; } = null!;
        public string? RedirectUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ReadDate { get; set; }
    }
}
