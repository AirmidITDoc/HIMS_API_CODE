using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class RequestForIPListDto
    {
        public string RegNo { get; set; }
        public string PatientName {  get; set; }
        public string AgeYear { get; set; } 
        public string GenderName { get; set; }
        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
        public string OPDNo { get; set; }
        public string DoctorName { get; set; }
        public bool? IsConvertRequestForIp { get; set; }
        public long VisitId { get; set; }
        public long RegID { get; set; }
        public string ConvertId { get; set; }
        public long AdmissionType { get; set; }
        public string RefDoctorName { get; set; }
        public string CompanyName { get; set; }
        public string PatientType { get; set; } 
 

    }
}
