using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class OpeningBalanaceItemDetailListDto
    {
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }
        public decimal? PerUnitPurRate { get; set; }
        public decimal PerUnitLandedRate { get; set; }
        public decimal? PerUnitMrp { get; set; }
        public float  CGSTPer { get; set; }
        public float  SGSTPer { get; set; }
        public float IGSTPer { get; set; }
        public double Gstper { get; set; }
        public float BalQty { get; set; }


    }
}
