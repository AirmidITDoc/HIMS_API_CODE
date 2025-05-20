using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class WorkorderIteListDto
    {
      
        public String? ItemName { get; set; }

        public long? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscPer { get; set; }

        public decimal? DiscAmount { get; set; }

        public decimal? VATPer { get; set; }
        public decimal? VATAmount { get; set; }


        public decimal? NetAmount { get; set; }

        public String? Remark { get; set; }
        public long? PendQty { get; set; }
    }
}
