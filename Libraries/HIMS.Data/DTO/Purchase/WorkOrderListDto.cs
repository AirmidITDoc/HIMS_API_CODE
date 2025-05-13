using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class WorkOrderListDto
    {
        public long WOId { get; set; }

        public String? WONo { get; set; }

        public String? Date { get; set; }
        public String? Time { get; set; }
        public String? WODate { get; set; }
        public long? StoreId { get; set; }

        public String? StoreName { get; set; }

        public long? SupplierId { get; set; }
        public String? SupplierName { get; set; }

        //public decimal? WOTotalAmount { get; set; }

        //public decimal? WOVatAmount { get; set; }
        //public decimal? WODiscAmount { get; set; }

        public decimal? WoNetAmount { get; set; }
        public String? WORemark { get; set; }

        

            
    }
}
