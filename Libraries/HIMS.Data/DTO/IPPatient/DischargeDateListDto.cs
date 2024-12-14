using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class DischargeDateListDto
    {
        public long AdmissionID { get; set; }
        public long RegID { get; set; }
        public long PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public string? PatientName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long GenderId { get; set; }
        public string? GenderName { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string? DOA { get; set; }
        public string? AdmissionTime { get; set; }
        public string? DOT { get; set; }
        public long PatientTypeID { get; set; }
        public long HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string RoomName { get; set; }
        public long WardId { get; set; }
        public long BedId { get; set; }
        public string BedName { get; set; }
        public string DocNameID { get; set; }
        public long RefDocNameId { get; set; }
        public string Doctorname { get; set; }
        public string RefDocName { get; set; }
        public byte? IsDischarged { get; set; }
        public byte? IsBillGenerated { get; set; }
        public DateTime DischargeDate { get; set; }
        public string DischargeTime { get; set; }
        public string AdmDateTime { get; set; }
        public string DischDateTime { get; set; }
        public long TariffId { get; set; }
        public long ClassId { get; set; }
        public string?TariffName { get; set; }
        public string?ClassName { get; set; }
        public string? MobileNo { get; set; }
        public long CityId { get; set; }
        public long ReligionId { get; set; }
        public long AreaId { get; set; }
        public long CompanyId { get; set; }
        public bool? IsMarkForDisNur { get; set; }
        public bool? IsCovidFlag { get; set; }
        public decimal? ApprovedAmount { get; set; }


    }
}
