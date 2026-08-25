using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpAdmissionDiagnosisInformation
    {
        public long IpdiagnosisId { get; set; }
        public long? AdmId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Icdcode { get; set; }
        public string? Diagnosisinformation { get; set; }
        public string? FlagCode { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
