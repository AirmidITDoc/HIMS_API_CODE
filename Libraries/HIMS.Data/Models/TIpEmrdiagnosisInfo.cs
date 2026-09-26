using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpEmrdiagnosisInfo
    {
        public long IpemrdiagnId { get; set; }
        public long Ipemrid { get; set; }
        public long AdmId { get; set; }
        public string? Diagnosis { get; set; }
        public string? Icdcode { get; set; }
        public string? Diagnosisinformation { get; set; }
        public string? FlagCode { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TIpEmrhistory Ipemr { get; set; } = null!;
    }
}
