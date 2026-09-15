using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MAllergyMaster
    {
        public long AllergyId { get; set; }
        public string AllergyCode { get; set; } = null!;
        public string AllergyName { get; set; } = null!;
        public long? CategoryId { get; set; }
        public long? SeverityId { get; set; }
        public string? Reaction { get; set; }
        public bool IsKitchenAlert { get; set; }
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
