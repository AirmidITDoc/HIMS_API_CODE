using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class CanteenRequestListDto
    {
       public string ItemName { get; set; }
        public decimal UnitMRP { get; set; }
        public double Qty { get; set; }
        public long ReqId { get; set; }

    }
}
