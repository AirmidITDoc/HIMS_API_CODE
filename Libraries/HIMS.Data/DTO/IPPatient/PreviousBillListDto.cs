using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class PreviousBillListDto
    {
      
        public string? ServiceName { get; set; }
        public long BillNo { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? AddDoctorName { get; set; }


    }

}
