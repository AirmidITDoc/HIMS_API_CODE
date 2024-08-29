using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MRadiologyTestMaster
    {
        public MRadiologyTestMaster()
        {
            MRadiologyTemplateDetails = new HashSet<MRadiologyTemplateDetail>();
        }

        public long TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
        public long? ServiceId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MRadiologyTemplateDetail> MRadiologyTemplateDetails { get; set; }
    }
}
