using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class AdmissionListDto
    {
        public long AdmissionId { get; set; }
        public long? RegId { get; set; }
        public bool? IsOpToIpconv { get; set; }
        public long PrefixId { get; set; }
        public string? PatientName { get; set; }
        public long? MaritalStatusId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public string? AadharCardNo { get; set; }
        public string? DateofBirth { get; set; }
        public string? GenderName { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? DOA { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public long? PatientTypeID { get; set; }
        public string? PatientType { get; set; }
        public long? HospitalID { get; set; }
        public string? HospitalName { get; set; }
        public long? AreaId { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public long? DocNameId { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public string? Ipdno { get; set; }
        public string? Doctorname { get; set; }
        public string? RefDocName { get; set; }
        public byte? IsDischarged { get; set; }
        public byte? IsBillGenerated { get; set; }
        public string? DischargeTime { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public string? TariffName { get; set; }
        public string? ClassName { get; set; }
        public string? RegNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? ReligionId { get; set; }
        public string? City { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? PinNo { get; set; }
        public string? Expr1 { get; set; }
        public string? RelativeName { get; set; }
        public string? RelatvieMobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public long? AdmittedDoctor1ID { get; set; }
        public string? AdmittedDoctor1 { get; set; }
        public long? AdmittedDoctor2ID { get; set; }
        public string? AdmittedDoctor2 { get; set; }
        public bool? IsMLC { get; set; }
        public long? SubTpaComId { get; set; }
        public string? PolicyNo { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? HosApreAmt { get; set; }
        public decimal? PathApreAmt { get; set; }
        public decimal? PharApreAmt { get; set; }
        public decimal? RadiApreAmt { get; set; }
        public byte? AdmissionType { get; set; }
        public string? EmgContactPersonName { get; set; }
        public long? EmgRelationshipId { get; set; }
        public string? EmgMobileNo { get; set; }
        public string? EmgLandlineNo { get; set; }
        public string? EngAddress { get; set; }
        public string? EmgAadharCardNo { get; set; }
        public string? EmgDrivingLicenceNo { get; set; }
        public string? MedTourismPassportNo { get; set; }
        public DateTime? MedTourismVisaIssueDate { get; set; }
        public DateTime? MedTourismVisaValidityDate { get; set; }
        public string? MedTourismNationalityId { get; set; }
        public long? MedTourismCitizenship { get; set; }
        public string? MedTourismPortOfEntry { get; set; }
        public DateTime? MedTourismDateOfEntry { get; set; }
        public string? MedTourismResidentialAddress { get; set; }
        public string? MedTourismOfficeWorkAddress { get; set; }
    }

    public class PatientAdmittedListSearchDto
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? RegNo { get; set; }
        public long AdmissionID { get; set; }
        public long RegID { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public long? PatientTypeID { get; set; }
        public long? HospitalID { get; set; }
        public long? DocNameID { get; set; }
        public long? RefDocNameID { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public byte? IsDischarged { get; set; }
        public string? MobileNo { get; set; }
        public string? IPDNo { get; set; }
        public string? CompanyName { get; set; }
        public string? TariffName { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public string? DoctorName { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public string? Age { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? GenderName { get; set; }

        public string? PatientType { get; set; }
        public string? DepartmentName { get; set; }

        public string? RefDoctorName { get; set; }
        
        public string FormattedText { get { return this.FirstName + " " + this.MiddleName + " " + this.LastName + " | " + this.RegNo + " | " + this.MobileNo; } }
    }
}
