using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class DayWiseCurrentStockDto
    {
        public DateTime? LedgerDate { get; set; }
        public long StroreId { get; set; }
        public long ItemId {  get; set; }

    }
}
