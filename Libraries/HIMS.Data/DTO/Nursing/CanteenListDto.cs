using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Nursing
{
    public class CanteenListDto
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public bool IsBatchRequired { get; set; }
        public long ItemID { get; set; }
    }
}
