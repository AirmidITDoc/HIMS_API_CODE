using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtbookingRequest
    {
        public long OtbookingId { get; set; }
        public DateTime? OtbookingDate { get; set; }
        public DateTime? OtbookingTime { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? SurgeryType { get; set; }
        public long? DepartmentId { get; set; }
        public long? CategoryId { get; set; }
        public long? SiteDescId { get; set; }
        public long? SurgeryId { get; set; }
        public long? SurgeonId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public DateTime? OtrequestDate { get; set; }
        public DateTime? OtrequestTime { get; set; }
        public long? OtrequestId { get; set; }
    }
}
