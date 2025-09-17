using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class OpeningBalHeaderModel
    {
        public long StoreId { get; set; }
        public DateTime OpeningDate { get; set; }
        public string OpeningTime { get; set; }
        public long? CreatedBy { get; set; }
        public long OpeningHid { get; set; }

    }


    public class OpeningTransactionDetails
    {
        public long? OpeningHeaderId { get; set; }
        public long? StoreId { get; set; }
        public DateTime? OpeningDate { get; set; }
        public string? OpeningTime { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? PerUnitMrp { get; set; }
        public decimal? PerUnitPurRate { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }
        public double? Gstper { get; set; }
        public float? TotalQty { get; set; }
        public float? Packing { get; set; }
        public float? StripQty { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }

    public class OpeningBalanceModel
    {
        public OpeningBalHeaderModel OpeningBal { get; set; }
        public List<OpeningTransactionDetails> OpeningTransaction { get; set; }

    }
}
