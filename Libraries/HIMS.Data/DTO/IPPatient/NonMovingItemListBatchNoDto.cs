using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class NonMovingItemListBatchNoDto
    {
        public DateTime? LastSalesDate { get; set; }
        public string? DaySales { get; set; }
        public float? BalanceQty { get; set; }
        public string? ItemName { get; set; }
    }
}
