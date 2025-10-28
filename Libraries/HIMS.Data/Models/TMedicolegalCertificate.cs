using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMedicolegalCertificate
    {
        public long DocId { get; set; }
        public DateTime? Mlcdate { get; set; }
        public DateTime? Mlctime { get; set; }
        public string? CertificateNo { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public DateTime? AccidentDate { get; set; }
        public DateTime? AccidentTime { get; set; }
        public string? DetailsInjuries { get; set; }
        public string? AgeofInjuries { get; set; }
        public string? CauseofInjuries { get; set; }
        public long? TreatingDoctorId { get; set; }
        public long? TreatingDoctorId1 { get; set; }
        public long? TreatingDoctorId2 { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
