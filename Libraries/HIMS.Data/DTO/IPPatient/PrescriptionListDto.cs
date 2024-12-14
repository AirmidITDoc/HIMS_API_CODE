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
        
        public string PatientName { get; set; }
        public string Vst_Adm_Date { get; set; }
        public string Date { get; set; }
        public long OP_IP_ID { get; set; }
        public byte OPD_IPD_Type { get; set; }
        public string StoreName { get; set; }
        public long IPMedID { get; set; }
        public string CompanyName { get; set; }
        public long CompanyId { get; set; }

    }
}
