using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpmedicalRecord
    {
        public TIpmedicalRecord()
        {
            TIpPrescriptions = new HashSet<TIpPrescription>();
        }

        public long MedicalRecoredId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? RoundVisitDate { get; set; }
        public DateTime? RoundVisitTime { get; set; }
        public bool? InHouseFlag { get; set; }
        public string? PresNo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TIpPrescription> TIpPrescriptions { get; set; }
    }
}
