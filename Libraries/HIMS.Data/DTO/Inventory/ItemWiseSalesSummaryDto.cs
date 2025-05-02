using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class ItemWiseSalesSummaryDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime todate { get; set; }
        public long StoreId { get; set; }
        public long ItemId { get; set; }
       
    }
}
