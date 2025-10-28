using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwIpmedicalRecordVisitde
    {
        public long AdmissionId { get; set; }
        public string? Pdate { get; set; }
        public string? Ptime { get; set; }
        public long MedicalRecoredId { get; set; }
    }
}
