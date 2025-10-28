namespace HIMS.Data.Models
{
    public partial class MCertificateMaster
    {
        public long CertificateId { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateDesc { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
