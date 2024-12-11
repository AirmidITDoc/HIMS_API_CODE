using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Master
{
    public  class DoctoreMasterDto
    {
        public long DoctorId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? DoctorName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public long? GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? Education { get; set; }
        public bool? IsConsultant { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? MahRegDate { get; set; }
        public bool? IsRefDoc { get; set; }
        public bool? IsActive { get; set; }
        public string? DoctorType { get; set; }
        public long DoctorTypeId { get; set; }
        public bool? IsInHouseDoctor { get; set; }
        public bool? IsOnCallDoctor { get; set; }
        public string? PassportNo { get; set; }
        public string? ESINO { get; set; }
        public string? RegNo { get; set; }
        public string? MAHREGNO { get; set; }
        public string? PANCARDNO { get; set; }
        public string AADHARCARDNO { get; set; }
        public string? Signature { get; set; }

    }
}
