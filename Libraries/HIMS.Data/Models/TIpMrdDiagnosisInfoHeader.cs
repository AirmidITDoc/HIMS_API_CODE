using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpMrdDiagnosisInfoHeader
    {
        public TIpMrdDiagnosisInfoHeader()
        {
            TIpMrdDiagnosisInfoDetails = new HashSet<TIpMrdDiagnosisInfoDetail>();
        }

        public long IpdiagId { get; set; }
        public long? AdmId { get; set; }
        public bool? IsSync { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TIpMrdDiagnosisInfoDetail> TIpMrdDiagnosisInfoDetails { get; set; }
    }
}
