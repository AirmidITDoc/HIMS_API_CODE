using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Common.Configuration;

namespace HIMS.Data.DTO.Inventory
{
    public class SalesReturnDetailsListDto
    {
        public string? Date { get; set; }
        public string? SalesReturnNo { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double? Qty { get; set; }
        public double? MRP { get; set; }
        public long? StoreID { get; set; }




    }
}
