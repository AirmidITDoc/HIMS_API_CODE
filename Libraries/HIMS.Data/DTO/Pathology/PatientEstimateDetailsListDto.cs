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

        public decimal TotalAmount { get; set; }

        public decimal DiscAmount { get; set; }
        public decimal NetAmount { get; set; }

        public string? Comments { get; set; }
        public decimal Price { get; set; }
        public long? Qty { get; set; }
        public string? ServiceName { get; set; }
    }
}
