using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class SalesReturnSummaryListDto
    {
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double? ReturnQty { get; set; }
        public long? StoreID { get; set; }



        
    }
}
