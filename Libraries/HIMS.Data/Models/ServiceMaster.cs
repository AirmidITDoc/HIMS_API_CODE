using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ServiceMaster
    {
        public ServiceMaster()
        {
            ServiceDetails = new HashSet<ServiceDetail>();
        }

        public long ServiceId { get; set; }
        public long? GroupId { get; set; }
        public string? ServiceShortDesc { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public bool? IsEditable { get; set; }
        public bool? CreditedtoDoctor { get; set; }
        public long? IsPathology { get; set; }
        public bool? IsPathOutSource { get; set; }
        public long? IsRadiology { get; set; }
        public bool? IsRadOutSource { get; set; }
        public bool? IsDiscount { get; set; }
        public long? IsPackage { get; set; }
        public long? SubGroupId { get; set; }
        public long? DoctorId { get; set; }
        public bool? IsEmergency { get; set; }
        public decimal? EmgAmt { get; set; }
        public double? EmgPer { get; set; }
        public DateTime? EmgStartTime { get; set; }
        public DateTime? EmgEndTime { get; set; }
        public long? PrintOrder { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDocEditable { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
    }
}
