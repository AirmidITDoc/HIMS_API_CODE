namespace HIMS.Data.DTO.OTManagement
{
    public class OTBookinglistDto
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
        public string? DoctorName { get; set; }
        public string? MobileNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? CompanyName { get; set; }
        public string? IPDNo { get; set; }
        public string? UnBooking { get; set; }
        public string? TariffName { get; set; }
        public string? UserName { get; set; }
    }

    public class OTRequestDetailsListSearchDto
    {
        public long OTRequestId { get; set; }
        public string? OTRequestDateTime { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? RegNo { get; set; }
        public string? GenderName { get; set; }
        public long? ReservationOTRequestId { get; set; }
        public string FormattedText { get { return this.PatientName + " | " + this.MobileNo + " | " + this.RegNo; } }
        public long? OTReservationId { get; set; }
        public long? OPIPType { get; set; }
        public long OPIPID { get; set; }

    }
    public class requestAttendentListDto
    {
        public long OTReservationId { get; set; }
        public long DoctorTypeId { get; set; }
        public string DoctorType { get; set; }
        public long DoctorId { get; set; }
        public long DoctorName { get; set; }


    }


}
