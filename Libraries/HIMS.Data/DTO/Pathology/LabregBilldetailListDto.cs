using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabregBilldetailListDto
    {
        public string? BillNo { get; set; }
        public DateTime BillTime { get; set; }
        public long ChargesId { get; set; }
        public string ServiceName { get; set; }


        public DateTime ChargesTime { get; set; }
        public double Price { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public string? DoctorName { get; set; }

        public bool? IsCompleted { get; set; }

        //public decimal? OnlinePay { get; set; }
    }
}
