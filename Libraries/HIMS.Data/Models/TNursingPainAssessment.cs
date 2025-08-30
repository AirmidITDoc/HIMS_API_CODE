using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TNursingPainAssessment
    {
        public long PainAssessmentId { get; set; }
        public DateTime? PainAssessmentDate { get; set; }
        public DateTime? PainAssessmentTime { get; set; }
        public long? AdmissionId { get; set; }
        public int? PainAssessementValue { get; set; }
        public bool? IsActive { get; set; }
        public string? Reason { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
