using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class EmergencyListDto
    {
        public long EmgId { get; set; }
        public long RegId { get; set; }
        public DateTime? EmgDate { get; set; }
        public DateTime? EmgTime { get; set; }
        public string? SeqNo { get; set; }
        public string? PatientName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? PrefixId { get; set; }
        public long? GenderID { get; set; }
        public long? CityId { get; set; }
        public int? AgeYear { get; set; }
        public int? AgeMonth { get; set; }
        public int? AgeDay { get; set; }
        public string? DoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public string? CityName { get; set; }
        public long CreatedBy { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Comment { get; set; }
        public int? Classid { get; set; }
        public int? Tariffid { get; set; }
        public long IsAfter24Hrs { get; set; }
        public long HoursSinceAdmission { get; set; }
        public string? GenderName { get; set; }
        public string? TariffName { get; set; }
        public string? ConvertedIntoAdm { get; set; }
        public string? AddedBy { get; set; }
        public long? RefDoctorId { get; set; }
        public long? AttendingDoctorId { get; set; }
        public bool? IsMlc { get; set; }
        public long? IsConverted { get; set; }
        public string? AttendingDoctorName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? RegNo { get; set; }




    }
}
