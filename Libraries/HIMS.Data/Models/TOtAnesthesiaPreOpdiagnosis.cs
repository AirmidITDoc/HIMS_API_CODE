using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtAnesthesiaPreOpdiagnosis
    {
        public long OtanesthesiaPreOpdiagnosisId { get; set; }
        public long? AnesthesiaId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TOtAnesthesiaRecord? Anesthesia { get; set; }
    }
}
