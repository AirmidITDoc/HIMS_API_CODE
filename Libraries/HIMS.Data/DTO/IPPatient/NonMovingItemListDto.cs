using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class NonMovingItemListDto
    {
        public DateTime? LastSalesDate { get; set; }
        public string? DaySales { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public float? BalanceQty { get; set; }
        public string? ItemName { get; set; }
     
    }
}
