using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMlcinformation
    {
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
