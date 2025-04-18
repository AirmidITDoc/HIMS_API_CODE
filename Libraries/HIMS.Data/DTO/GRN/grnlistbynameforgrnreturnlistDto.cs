using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.GRN
{
    public class grnlistbynameforgrnreturnlistDto
    {
        public long GRNID { get; set; }
        public String? GrnNumber { get; set; }
        public String? GRNDate { get; set; }
        public long? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? Cash_CreditType { get; set; }
        public long? StoreId { get; set; }
       
    }
}
