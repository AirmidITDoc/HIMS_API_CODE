using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class PharCurrentDetListDto
    {

        public DateTime Date { get; set; }
        public string SalesNo { get; set; }
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public double Qty { get; set; }
        public decimal UnitMRP { get; set; }
        public long StoreId { get; set; }
    }
}
