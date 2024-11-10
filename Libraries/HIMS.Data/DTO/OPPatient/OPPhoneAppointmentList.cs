using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OPPhoneAppointmentList
    {

        public long PhoneAppId { get; set; }
        public string? PatientName { get; set; }

        public string? Address { get; set; }

        public string? MobileNo { get; set; }

        public string? DepartmentName { get; set; }

    }
}
