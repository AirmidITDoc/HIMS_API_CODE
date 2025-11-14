using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtReservationCheckInOut
    {
        public long OtcheckInId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? OtcheckInDate { get; set; }
        public DateTime? OtcheckInTime { get; set; }
        public string? OtcheckInNo { get; set; }
        public byte? Opipid { get; set; }
        public bool? Opiptype { get; set; }
        public long? FromDepartment { get; set; }
        public long? ToDepartment { get; set; }
        public string? MovingType { get; set; }
        public string? ModeOfTransfer { get; set; }
        public long? AuthorisedBy { get; set; }
        public long? Accompanied { get; set; }
        public string? EquipmentCarried { get; set; }
        public string? Remark { get; set; }
        public string? PurPoseOfMovement { get; set; }
        public byte? CheckInOut { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public long? CheckOutFromDepartment { get; set; }
        public long? CheckOutToDepartment { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
