﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtRequestDiagnosis
    {
        public long OtrequestDiagnosisDetId { get; set; }
        public long? OtrequestId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
        public long? Createdby { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual TOtRequestHeader? Otrequest { get; set; }
    }
}
