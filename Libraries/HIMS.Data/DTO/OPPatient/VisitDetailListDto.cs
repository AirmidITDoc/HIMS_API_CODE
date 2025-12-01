namespace HIMS.Data.DTO.OPPatient
{
    public class VisitDetailListDto
    {

        public long VisitId { get; set; }
        public long RegId { get; set; }
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public long PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public string? AadharCardNo { get; set; }
        public DateTime DateofBirth { get; set; }
        public string? Address { get; set; }
        public long MaritalStatusId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime VisitDate { get; set; }
        public string DVisitDate { get; set; }
        public string VisitTime { get; set; }
        public long HospitalId { get; set; }
        public string HospitalName { get; set; }
        public long PatientTypeId { get; set; }
        public string PatientType { get; set; }
        public string VistDateTime { get; set; }
        public string OPDNo { get; set; }
        public long TariffId { get; set; }
        public string? TariffName { get; set; }
        public long DepartmentId { get; set; }
        public long AppPurposeId { get; set; }
        public long CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public long ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? RegNoWithPrefix { get; set; }
        public long CityId { get; set; }
        public long ReligionId { get; set; }
        public long AreaId { get; set; }
        public string? FollowupDate { get; set; }
        public bool? IsMark { get; set; }
        public string? MPbillNo { get; set; }
        public long? PatientOldNew { get; set; }
        public long OldPatCnt { get; set; }
        public string? NewPatCnt { get; set; }
        public bool? IsCancelled { get; set; }
        public long CrossConsulCnt { get; set; }
        public string? MobileNo { get; set; }
        public string? DepartmentName { get; set; }
        public long DoctorId { get; set; }
        public string? Doctorname { get; set; }
        public long RefDocId { get; set; }
        public string? RefDocName { get; set; }
        public long PhoneAppId { get; set; }
        public long CrossConsulFlag { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public DateTime? ConStartTime { get; set; }
        public DateTime? ConEndTime { get; set; }
        public long CampId { get; set; }
        public string CampName { get; set; }
        public bool? IsConvertRequestForIp { get; set; }
        public long EMRReady { get; set; }
        public long TokenNo { get; set; }
        public long? GenderId { get; set; }





    }
    public class VisitDetailsListSearchDto
    {
        public string? FirstName { get; set; }
        public string? RegNo { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long VisitId { get; set; }
        public long? RegId { get; set; }
        public string FormattedText { get { return this.FirstName + " " + this.MiddleName + " " + this.LastName + " | " + this.MobileNo + " | " + this.RegNo; } }
        public string? MobileNo { get; set; }
        public long? PatientTypeId { get; set; }
        public long? ConsultantDocId { get; set; }
        public long? RefDocId { get; set; }
        public string? OPDNo { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public string? TariffName { get; set; }
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? DepartmentName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? DoctorName { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public long? UnitId { get; set; }
        public long? hospitalId { get; set; }

    }
}
