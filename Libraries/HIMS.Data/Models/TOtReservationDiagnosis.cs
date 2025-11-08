using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtReservationDiagnosis
    {
        public long OtreservationDiagnosisDetId { get; set; }
        public long? OtreservationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TOtReservationHeader? Otreservation { get; set; }
    }
}
