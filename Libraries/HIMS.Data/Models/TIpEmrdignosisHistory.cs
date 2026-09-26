using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpEmrdignosisHistory
    {
        public long EmrdignId { get; set; }
        public long Ipemrid { get; set; }
        public long AdmissionId { get; set; }
        public string? DescriptionName { get; set; }
        public string DescriptionType { get; set; } = null!;
        public string? Icdcode { get; set; }
        public string? DiagnosisName { get; set; }
        public string? DiagnosisInfo { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TIpEmrhistory Ipemr { get; set; } = null!;
    }
}
