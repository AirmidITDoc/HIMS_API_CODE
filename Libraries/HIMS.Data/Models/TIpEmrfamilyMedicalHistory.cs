using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpEmrfamilyMedicalHistory
    {
        public long FhistId { get; set; }
        public long IpEmrId { get; set; }
        public long AdmissionId { get; set; }
        public long RelationshipId { get; set; }
        public string MemberName { get; set; } = null!;
        public long Age { get; set; }
        public string? ClinicalHistory { get; set; }
        public long? Duration { get; set; }
        public long GenderId { get; set; }
        public string? Summary { get; set; }
        public bool Status { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TIpEmrhistory Fhist { get; set; } = null!;
    }
}
