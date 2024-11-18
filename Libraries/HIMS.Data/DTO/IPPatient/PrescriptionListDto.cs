using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionListDto
    {
        public string RegNo { get; set; }
        //public string PrefixName { get; set; }
        public string PatientName { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime Date { get; set; }
        public long OP_IP_ID { get; set; }
        public byte OPD_IPD_Type { get; set; }
        public string StoreName { get; set; }
        public long IPMedID { get; set; }

    }
}
