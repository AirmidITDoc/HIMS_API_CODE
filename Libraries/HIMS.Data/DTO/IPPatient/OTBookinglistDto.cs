using HIMS.Data.Models;
using LinqToDB.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class OTBookinglistDto
    {

        public long OtreservationId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ReservationTime { get; set; }
        public long? OpIpId { get; set; }
        public bool? OpIpType { get; set; }
        public DateTime? Opdate { get; set; }
        public DateTime? OpstartTime { get; set; }
        public DateTime? OpendTime { get; set; }
        public long? OTTypeID { get; set; }
        public int? Duration { get; set; }
        public long? OttableId { get; set; }
        public long? SurgeryId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public string? SurgeryName { get; set; }
        public string? DepartmentName { get; set; }
        public long? AdmissionID { get; set; }
        public string? OTTableName { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public long? DoctorId { get; set; }
        public string SurgenName { get; set; }
        public string SurgenName1 { get; set; }
        public string? AnestheticsDr { get; set; }
        public string AnestheticsDr1 { get; set; }
        public long? InstructionId { get; set; }
        public string? Instruction { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeonId1 { get; set; }
        public long? AnesthTypeId { get; set; }
        public long? AnestheticsDrID { get; set; }
        public long? AnestheticsDrID1 { get; set; }

        





    }
}
