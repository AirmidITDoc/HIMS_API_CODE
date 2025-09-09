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
        public string ItemName {  get; set; }
        public string ConversionFactor { get; set; }
        public double Current_BalQty { get; set; }
        public double? Received_Qty { get; set; }
        public double? Sales_Qty { get; set; }


    }
}
