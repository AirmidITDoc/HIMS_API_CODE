namespace HIMS.Data.Models
{
    public partial class TConsentInformation
    {
        public long ConsentId { get; set; }
        public DateTime? ConsentDate { get; set; }
        public DateTime? ConsentTime { get; set; }
        public long? Opipid { get; set; }
        public long? Opiptype { get; set; }
        public long? ConsentDeptId { get; set; }
        public long? ConsentTempId { get; set; }
        public string? ConsentName { get; set; }
        public string? ConsentText { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
