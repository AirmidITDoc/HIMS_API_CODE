using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSurgeryMaster
    {
        public long SurgeryId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public string? SurgeryName { get; set; }
        public long? DepartmentId { get; set; }
        public decimal? SurgeryAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public long? SiteDescId { get; set; }
        public long? OttemplateId { get; set; }
    }
}
