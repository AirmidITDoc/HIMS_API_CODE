using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TNursingWeight
    {
        public long PatWeightId { get; set; }
        public DateTime? PatWeightDate { get; set; }
        public DateTime? PatWeightTime { get; set; }
        public long? AdmissionId { get; set; }
        public int? PatWeightValue { get; set; }
        public bool? IsActive { get; set; }
        public string? Reason { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
