using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCertificateInformation
    {
        public long CertificateId { get; set; }
        public DateTime? CertificateDate { get; set; }
        public DateTime? CertificateTime { get; set; }
        public long? VisitId { get; set; }
        public string? CertificateName { get; set; }
        public string? CertificateText { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
