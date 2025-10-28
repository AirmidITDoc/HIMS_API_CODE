namespace HIMS.Data.Models
{
    public partial class MessageConfiguration
    {
        public long MessageConfigId { get; set; }
        public string? VendorName { get; set; }
        public string? MessageType { get; set; }
        public string? OriginalUrl { get; set; }
        public string? BaseUrl { get; set; }
        public string? ParameterUrl { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
