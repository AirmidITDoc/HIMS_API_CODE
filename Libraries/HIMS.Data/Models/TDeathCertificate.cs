using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDeathCertificate
    {
        public long CertificateId { get; set; }
        public string? CertificateNo { get; set; }
        public DateTime? CertificateDate { get; set; }
        public DateTime? CertificateTime { get; set; }
        public long? OpIpId { get; set; }
        public bool? OpIpType { get; set; }
        public DateTime? DateofDeath { get; set; }
        public DateTime? TimeOfDeath { get; set; }
        public string? CauseofDeath { get; set; }
        public string? PlaceOfDeath { get; set; }
        public string? ResponsiblePersonName { get; set; }
        public string? Smcno { get; set; }
        public string? Diagnsis { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
