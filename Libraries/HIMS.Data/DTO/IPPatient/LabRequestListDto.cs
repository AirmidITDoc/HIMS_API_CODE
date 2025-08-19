using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class LabRequestListDto
    {
        public string PatientName { get; set; }
        public string RegNo { get; set; }
        public DateTime ReqDate { get; set; }
        public DateTime ReqTime { get; set; }
        public long RequestId { get; set; }
        public long OPIPID { get; set; }
        public long OPIPType { get; set; }
        public string AdmDate { get; set; }
        public string WardName { get; set; }
        public string BedName { get; set; }
        public string RequestType { get; set; }
        public bool IsOnFileTest { get; set; }
        public bool IsCancelled { get; set; }
        public string IPDNo { get; set; }
        public string DoctorName { get; set; }
        public string CompanyName { get; set; }
        public string PatientType { get; set; }



    }

}