using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtReservation
    {
        public long OtreservationId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ReservationTime { get; set; }
        public long? OpIpId { get; set; }
        public bool? OpIpType { get; set; }
        public DateTime? Opdate { get; set; }
        public DateTime? OpstartTime { get; set; }
        public DateTime? OpendTime { get; set; }
        public int? Duration { get; set; }
        public long? OttableId { get; set; }
        public long? DepartmentId { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeonId1 { get; set; }
        public long? AnestheticsDr { get; set; }
        public long? AnestheticsDr1 { get; set; }
        public long? SurgeryId { get; set; }
        public long? AnesthTypeId { get; set; }
        public string? Instruction { get; set; }
        public long? OttypeId { get; set; }
        public bool? UnBooking { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public long? OtrequestId { get; set; }
        public string? Reason { get; set; }
    }
}
