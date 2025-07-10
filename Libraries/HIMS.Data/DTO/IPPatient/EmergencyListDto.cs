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
        public long? RegId { get; set; }
        public DateTime? EmgDate { get; set; }
        public DateTime? EmgTime { get; set; }
        public string? SeqNo { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? PrefixId { get; set; }
        public string? City { get; set; }
        public long? GenderID { get; set; }
        public long? CityId { get; set; }
        public long? AgeYear { get; set; }
        public string? DoctorName { get; set; }
        public string? DepartmentName { get; set; }




       
    }
}
