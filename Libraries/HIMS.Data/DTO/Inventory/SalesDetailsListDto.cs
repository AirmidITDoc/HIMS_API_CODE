using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class SalesDetailsListDto
    {
        public string? Date { get; set; }
        public string? SalesReturnNo { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double Qty { get; set; }
        public Decimal? MRP { get; set; }
        public long? StoreID { get; set; }
    }
}
