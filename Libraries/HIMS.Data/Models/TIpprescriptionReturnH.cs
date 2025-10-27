namespace HIMS.Data.Models
{
    public partial class TIpprescriptionReturnH
    {
        public TIpprescriptionReturnH()
        {
            TIpprescriptionReturnDs = new HashSet<TIpprescriptionReturnD>();
        }

        public long PresReId { get; set; }
        public string? PresNo { get; set; }
        public DateTime? PresDate { get; set; }
        public DateTime? PresTime { get; set; }
        public long? ToStoreId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? Addedby { get; set; }
        public bool? IsActive { get; set; }
        public bool? Isclosed { get; set; }

        public virtual ICollection<TIpprescriptionReturnD> TIpprescriptionReturnDs { get; set; }
    }
}
