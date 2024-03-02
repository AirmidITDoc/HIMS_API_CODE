using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwMedicalCertificate
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
        public long Mlcid { get; set; }
        public long? AdmissionId { get; set; }
        public string? Mlcno { get; set; }
        public DateTime? ReportingDate { get; set; }
        public DateTime? ReportingTime { get; set; }
        public string? AuthorityName { get; set; }
        public string? BuckleNo { get; set; }
        public string? PoliceStation { get; set; }
    }
}
