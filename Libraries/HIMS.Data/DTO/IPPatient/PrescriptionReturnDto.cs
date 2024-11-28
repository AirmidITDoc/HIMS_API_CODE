using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PrescriptionReturnDto
    {
        public long PresReId { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double? Qty { get; set; }
    }
}
