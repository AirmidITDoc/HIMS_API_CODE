using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpMrdDiagnosisInfoDetail
    {
        public long IpdiagDetId { get; set; }
        public long IpdiagId { get; set; }
        public long AdmId { get; set; }
        public string Diagnosis { get; set; } = null!;
        public string Icdcode { get; set; } = null!;
        public string Diagnosisinformation { get; set; } = null!;
        public string FlagCode { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TIpMrdDiagnosisInfoHeader Ipdiag { get; set; } = null!;
    }
}
