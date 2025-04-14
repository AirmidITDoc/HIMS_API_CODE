using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class MaterialConsumptionListDto
    {
        public long MaterialConsumptionId { get; set; }
        public string ConsumptionNo { get; set; }
        public string ConsumptionDate { get; set; }
       // public DateTime ConsumptionDate { get; set; }
        public DateTime ConsumptionTime { get; set; }
        public long FromStoreId { get; set; }
        public string StoreName { get; set; }
        public decimal LandedTotalAmount { get; set; }
        public string Remark { get; set; }
        public long AddedBy { get; set; }
        public long AdmId { get; set; }


    }
}
