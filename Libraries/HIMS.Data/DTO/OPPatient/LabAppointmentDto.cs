using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public  class LabAppointmentDto
    {
        public long LabAppId { get; set; }
       
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
      
        public long? DoctorId { get; set; }
       
    }
}
