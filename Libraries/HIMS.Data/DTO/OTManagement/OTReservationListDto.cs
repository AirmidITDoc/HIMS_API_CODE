using System.Xml.Linq;

namespace HIMS.Data.DTO.OTManagement
{
    public class OTReservationListDto
    {

        public long OTReservationId { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        public string OTReservationDateTime { get; set; }
        public string OTReservationNo { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public string? BloodGroup { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public string? TypeName { get; set; }
        public string? Duration { get; set; }
        public long? OttableId { get; set; }
        public string? OTTableName { get; set; }
        public string? SurgeryDate { get; set; }
        public string EstimateTime { get; set; }
        public string? Comments { get; set; }
        public bool? Pacrequired { get; set; }
        public bool? EquipmentsRequired { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public bool? Infective { get; set; }
        public long? Createdby { get; set; }
        public string? UserName { get; set; }
        public string VisitDate { get; set; }
        public string? OPDNo { get; set; }
        public string PatientName { get; set; }
        public string RoomName { get; set; }
        public string? BedName { get; set; }
        public string RegNo { get; set; }
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
        public string? DepartmentName { get; set; }
        public string? TariffName { get; set; }
        public string? CompanyName { get; set; }
        public string? PatientType { get; set; }
        public string? GenderName { get; set; }
        public long? OTRequestId { get; set; }
        public long? OTCheckInId { get; set; }
        public byte? CheckInOut { get; set; }
        public long OTPreOperationId { get; set; }
        public long OTInOperationId { get; set; }
        public long AnesthesiaId { get; set; }
        public bool? IsAnaesthetistPaid { get; set; }
        public bool? IsMaterialReplacement { get; set; }





    }

    //public class OTRequestDetailsListSearchDto
    //{
    //    public long OTRequestId { get; set; }
    //    public string? OTRequestDateTime { get; set; }
    //    public string? PatientName { get; set; }
    //    public string? MobileNo { get; set; }
    //    public string? RegNo { get; set; }
    //    public string? GenderName { get; set; }
    //    public long? ReservationOTRequestId { get; set; }
    //    public string FormattedText { get { return this.PatientName + " | " + this.MobileNo + " | " + this.RegNo; } }
    //    public long? OTReservationId { get; set; }
    //    public long? OPIPType { get; set; }
    //    public long OPIPID { get; set; }

    //}
    public class OTRequestDetailsListSearchDto
    {
        public long OTRequestId { get; set; }
        public string OTRequestDateTime { get; set; }
        public string PatientName { get; set; }
        public string MobileNo { get; set; }
        public string GenderName { get; set; }
        public long ReservationOTRequestId { get; set; }
        public long OTReservationId { get; set; }
        public string OPIPType { get; set; }
        public long OPIPID { get; set; }
        public string RegNo { get; set; }
    }
    public class requestAttendentListDto
    {
        public long OTReservationId { get; set; }
        public long DoctorTypeId { get; set; }
        public string DoctorType { get; set; }
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }


    }
    public class ReservationSurgeryDetailListDto
    {
        public long OTReservationId { get; set; }
        public long SurgeryCategoryId { get; set; }
        public string SurgeryCategoryName { get; set; }
        public long SurgeryId { get; set; }
        public string SurgeryName { get; set; }
        public string SurgeryPart { get; set; }
        public string SurgeryFromTime { get; set; }
        public string SurgeryEndTime { get; set; }
        public string SurgeryDuration { get; set; }
        public string IsPrimary { get; set; }
        public long SurgeonId { get; set; }
        public string SurgeonName { get; set; }
        public long AnesthetistId { get; set; }
        public string AnestheticsName { get; set; }



    }

}
