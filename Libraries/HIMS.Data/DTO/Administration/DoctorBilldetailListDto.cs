using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class DoctorBilldetailListDto
    {
        public long ChargesId { get; set; }
      
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }


        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
        public long? DoctorId { get; set; }
        public string? PatientName { get; set; }
        public string? AddChargeDrName { get; set; }
      
    }
}
