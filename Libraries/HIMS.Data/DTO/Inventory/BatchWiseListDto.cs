using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class BatchWiseListDto
    {
        public long ItemId {  get; set; }
        public long StoreId { get; set; }
        public string? BatchNo { get; set; }
        public string ExpDate { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? PurUnitRate { get; set; }
        public float TotalByMRP { get; set; }
        public float TotalByPTR { get; set; }



    }
}
