using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtPreOperationCathlabDiagnosis
    {
        public long OtpreOperationCathLabDiagnosisDetId { get; set; }
        public long? OtpreOperationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TOtPreOperationHeader? OtpreOperation { get; set; }
    }
}
