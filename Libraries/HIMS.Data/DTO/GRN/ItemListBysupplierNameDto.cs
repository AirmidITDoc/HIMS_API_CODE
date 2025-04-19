using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.GRN
{
    public class ItemListBysupplierNameDto
    {
        public long GRNID { get; set; }
        public String? GrnNumber { get; set; }
        public String GRNDate { get; set; }
        public String? GRNTime { get; set; }
        public long? GRNDetID { get; set; }
        public long? ItemId { get; set; }
        public String? ItemName { get; set; }
        public String? BatchNo { get; set; }
        public String? BatchExpDate { get; set; }
        //public long? UOMId { get; set; }
        //public double? MRP { get; set; }
        //public double? Rate { get; set; }
        //public double TotalAmount { get; set; }
        //public long? ConversionFactor { get; set; }
        //public double VatPer { get; set; }
        //public double? VatAmount { get; set; }
        //public double? DiscPercentage { get; set; }
        //public double? DiscAmount { get; set; }
        //public double? OtherTax { get; set; }
        //public double? LandedRate { get; set; }
        //public double? NetAmount { get; set; }
        //public double? GrossAmount { get; set; }
        //public double? BalanceQty { get; set; }
        //public double? TotalQty { get; set; }

        //public bool? IsBatchRequired { get; set; }
        //public double? StoreId { get; set; }
        //public double? StkID { get; set; }
        //public double? ReturnQty { get; set; }


    }
}
