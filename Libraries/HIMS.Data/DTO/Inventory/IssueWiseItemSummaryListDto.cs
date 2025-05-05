using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class IssueWiseItemSummaryListDto
    {
        public long StoreId {  get; set; }
        public long ItemId { get; set; }
        public long ItemName {  get; set; }
        public string ConversionFactor { get; set; }
        public float Current_BalQty { get; set; }
        public float? ReceivedQty { get; set; }
        public float Sales_Qty { get; set; }

     
    }
}
