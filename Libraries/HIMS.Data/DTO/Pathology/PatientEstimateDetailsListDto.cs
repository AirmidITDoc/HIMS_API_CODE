using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class PatientEstimateDetailsListDto
    {
        //public string? BillNo { get; set; }
        //public DateTime BillTime { get; set; }
        public long EstimateId { get; set; }
        public string EstimateNo { get; set; }
        //public DateTime ChargesTime { get; set; }
        public double TotalAmount { get; set; }

        public double DiscAmount { get; set; }
        public double NetAmount { get; set; }

        public string? Comments { get; set; }
        public double? Price { get; set; }
        public long? Qty { get; set; }
        public string? ServiceName { get; set; }
    }
}
