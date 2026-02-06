using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class ItemNameBatchPOPIPPresReturnDto
    {
        public long ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? UnitMRP { get; set; }
        public decimal? VatPer { get; set; }
        public double? Qty { get; set; }
        public decimal? LandedPrice { get; set; }
        public decimal? PurRateWf { get; set; }
        public long StoreId { get; set; }
    }
}
