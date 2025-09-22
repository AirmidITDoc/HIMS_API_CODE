using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.GRN
{
    public class GRNReturnListDto
    {
        public long GRNReturnId { get; set; }
        public long? GRNReturnDetailId { get; set; }
        public long? GRNId { get; set; }
        public long? ItemId { get; set; }
        public String? ItemName { get; set; }
        public String? BatchNo { get; set; }
        public String? BatchExpiryDate { get; set; }
        public float? ReturnQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? MRP { get; set; }
        public decimal? UnitPurchaseRate { get; set; }
        public float? GSTPercentage { get; set; }
        public decimal? GSTAmount { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? MRPTotalAmount { get; set; }
        public decimal? PurchaseTotalAmount { get; set; }
        public short? Conversion { get; set; }
        public String? Remarks { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public long? StkId { get; set; }
        public float? TotalQty { get; set; }
        public decimal? NetAmount { get; set; }
        public float? DiscPercenetage { get; set; }
        public decimal? DiscAmount { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }

    }
}
