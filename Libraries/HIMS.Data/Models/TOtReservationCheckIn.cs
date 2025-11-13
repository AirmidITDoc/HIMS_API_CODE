using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtReservationCheckIn
    {
        public long OtcheckInId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? OtmovementDate { get; set; }
        public DateTime? OtmovementTime { get; set; }
        public string? OtcheckInNo { get; set; }
        public byte? Opipid { get; set; }
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
        public DateTime? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
