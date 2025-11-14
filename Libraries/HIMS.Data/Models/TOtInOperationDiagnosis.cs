using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtInOperationDiagnosis
    {
        public long OtinOperationDiagnosisDetId { get; set; }
        public long? OtinOperationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TOtInOperationHeader? OtinOperation { get; set; }
    }
}
