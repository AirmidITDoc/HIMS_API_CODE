using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class GRNDetailsListDto
    {
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }
        public float? ReceiveQty { get; set; }
        public float? FreeQty { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public long? ConversionFactor { get; set; }
        public double? VatPercentage { get; set; }
        public double? DiscPercentage { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? NetAmount { get; set; }
        public float? TotalQty { get; set; }
        public long? stockid { get; set; }
        public long? GRNDetID { get; set; }
        public bool? IsVerified { get; set; }
        public string? IsVerifiedDatetime { get; set; }
        public long? IsVerifiedUserId { get; set; }
        public decimal? VatAmount { get; set; }


    }
}
