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
        public string? Wono { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? WODate { get; set; }
        public long? StoreId { get; set; }
        public string? StoreName { get; set; }
        public long? SupplierId { get; set; }
        public string? SupplierName { get; set; }

        public decimal? WOTotalAmount { get; set; }

        public decimal? WOVatAmount { get; set; }
        public decimal? WODiscAmount { get; set; }

        public decimal? WoNetAmount { get; set; }
        public string? WORemark { get; set; }

        

            
    }
}
