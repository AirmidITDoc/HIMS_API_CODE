using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class PhoneAppointment2ListDto
    {
        public long PhoneAppId { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime? RegTime { get; set; }
        public string? PatientName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Age { get; set; }
        public long GenderName { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
    }
}
