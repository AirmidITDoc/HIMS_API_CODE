using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class THomeCollectPatientRegistartionDetail
    {
        public long PatientRegDetId { get; set; }
        public long? PatientRegId { get; set; }
        public bool? Priority { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public long? PatientType { get; set; }
    }
}
