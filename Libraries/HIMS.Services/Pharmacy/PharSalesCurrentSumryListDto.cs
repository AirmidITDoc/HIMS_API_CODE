using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class PharSalesCurrentSumryListDto
    {
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public double SalesQty { get; set; }
        public long StoreId { get; set; }
   
    }
}
