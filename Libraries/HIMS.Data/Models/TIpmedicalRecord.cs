using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpmedicalRecord
    {
        public long MedicalRecoredId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? RoundVisitDate { get; set; }
        public DateTime? RoundVisitTime { get; set; }
        public bool? InHouseFlag { get; set; }
    }
}
