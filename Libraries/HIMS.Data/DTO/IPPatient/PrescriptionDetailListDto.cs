using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionDetailListDto
    {
        public string ItemName { get; set; }
        public long MedicalRecoredId { get; set; }
        public long IPMedID { get; set; }
        public long OP_IP_ID { get; set; }

    }
}
