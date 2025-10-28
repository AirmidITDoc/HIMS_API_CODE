namespace HIMS.Data.Models
{
    public partial class TCertificateInformation
    {
        public long CertificateId { get; set; }
        public long? VisitId { get; set; }
        public long? CertificateTemplateId { get; set; }
        public DateTime? CertificateDate { get; set; }
        public DateTime? CertificateTime { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateText { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
