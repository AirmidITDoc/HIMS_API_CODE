using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOtSurgeryMaster
    {
        public long SurgeryId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public string? SurgeryName { get; set; }
        public long? DepartmentId { get; set; }
        public decimal? SurgeryAmount { get; set; }
        public long? SiteDescId { get; set; }
        public long? OttemplateId { get; set; }
        public long? ServiceId { get; set; }
        public long? TotalDuration { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
    }
}
