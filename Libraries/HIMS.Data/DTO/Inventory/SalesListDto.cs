using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class SalesListDto
    {
        public string SalesNo {  get; set; }
        public string Date { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string MobileNo { get; set; }
        public double Qty { get; set; }
        public decimal? TotalAmount { get; set; }



    }
}
