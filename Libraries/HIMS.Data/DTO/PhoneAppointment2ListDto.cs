using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO
{
    public class PhoneAppointment2ListDto
    {
        public long PhoneAppId { get; set; }
        public DateTime AppDate { get; set; }
        public DateTime AppTime { get; set; }
        public long SeqNo { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public long MobileNo { get; set; }
        public DateTime PhAppDate { get; set; }
        public DateTime PhAppTime { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }
        public long Mobile { get; set; }
        public long IsCancelled { get; set; }
        public string AddedByName { get; set; }
    }
}
