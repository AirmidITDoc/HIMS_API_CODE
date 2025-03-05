using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionReturnListDto
    {

        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public long? PresReId { get; set; }
        public string? Date { get; set; }
        public DateTime? PresTime { get; set; }
        public long? OP_IP_Id { get; set; }
        public string? Vst_Adm_Date { get; set; }
        public string? StoreName { get; set; }
        public byte? OP_IP_Type { get; set; }



    }
}
