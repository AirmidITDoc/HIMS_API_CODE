using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class View3
    {
        public long? RegId { get; set; }
        public long? Opipid { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? OtcheckInDate { get; set; }
        public DateTime? OtcheckInTime { get; set; }
        public string? OtcheckInNo { get; set; }
        public string? MovingType { get; set; }
        public string? ModeOfTransfer { get; set; }
        public long? AuthorisedBy { get; set; }
        public long? Accompanied { get; set; }
        public string? Remark { get; set; }
        public string? PurPoseOfMovement { get; set; }
        public byte? CheckInOut { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? GenderName { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? FromDepartment { get; set; }
        public long? ToDepartment { get; set; }
        public string? Expr1 { get; set; }
        public string? Expr2 { get; set; }
        public string? Ipdno { get; set; }
    }
}
