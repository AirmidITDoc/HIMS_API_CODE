using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Data.DTO.IPPatient
{
    public class DiscountTransactionHistoryListDto
    {
        public long? BillNo { get; set; }
        public string? PBillNo { get; set; }
        public decimal? DiscountAmt { get; set; }
        public decimal? CompDiscountAmt { get; set; }
        public string? ConcessionReason { get; set; }
        public string? UserName { get; set; }
     
    }
}
 				
