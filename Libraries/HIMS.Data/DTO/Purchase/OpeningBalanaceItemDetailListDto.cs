using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class OpeningBalanaceItemDetailListDto
    {
        public long OpeningHId { get; set; }

        public String? ItemName { get; set; }

        public String? BatchNo { get; set; }
        public String? BatchExpDate { get; set; }
        public decimal? PerUnitPurRate { get; set; }
        public decimal? PerUnitMrp { get; set; }
        public double? VatPer { get; set; }

        //public long? Qty { get; set; }


    }
}
