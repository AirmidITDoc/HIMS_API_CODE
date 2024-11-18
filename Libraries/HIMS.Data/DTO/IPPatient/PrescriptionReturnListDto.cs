using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionReturnListDto
    {
        public string PrefixName { get; set; }
        public string PatientName { get; set; }
        public long RegNo { get; set; }
        public long PresReId { get; set; }
        public DateTime PresDate { get; set; }
        public DateTime PresTime { get; set; }
        public long OP_IP_Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string StoreName { get; set; }
        public byte OP_IP_Type { get; set; }
    }
}
