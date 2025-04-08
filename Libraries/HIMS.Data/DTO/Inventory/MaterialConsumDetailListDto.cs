using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class MaterialConsumDetailListDto
    {

        public long MaterialConsumptionId { get; set; }
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public string BatchExpDate { get; set; }
        public long Qty { get; set; }
        public decimal PerUnitLandedRate { get; set; }
        public decimal PerUnitPurchaseRate { get; set; }
        public decimal PerUnitMRPRate { get; set; }
        public decimal LandedTotalAmount { get; set; }
        public decimal PurTotalAmount { get; set; }

        public decimal MRPTotalAmount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public String Remark { get; set; }
        public long AddedBy { get; set; }
    }
}
