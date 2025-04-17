using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class MedicationChartListDto
    {
        public long AdmissionID { get; set; }
        public long ItemId { get; set; }
        public string? ItemName {  get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }
        public double Qty { get; set; }
        public decimal UnitMRP { get; set; }

    }
}
