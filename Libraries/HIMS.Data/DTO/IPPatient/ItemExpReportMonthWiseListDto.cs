using HIMS.Data.Models;

namespace HIMS.Data.DTO.IPPatient
{
    public class ItemExpReportMonthWiseListDto
    {

       
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public string? StoreName { get; set; }
        public long? ItemID { get; set; }
        public string? ItemName { get; set; }

        public float? BalanceQty { get; set; }

        public float? ReceivedQty { get; set; }
        public string? BatchNo { get; set; }

        public Int32? ExpMonth { get; set; }

        public Int32? ExpYear { get; set; }

        //public Int32? BatchExpDate { get; set; }


        //public long? SalesQty { get; set; }
        public string? ConversionFactor { get; set; }



        //public float? Cgstper { get; set; }
        //public float? Sgstper { get; set; }
        //public float? Igstper { get; set; }
        //public long? BarCodeSeqNo { get; set; }
        //public long? IstkId { get; set; }
        //public float? OpeningBalance { get; set; }
        //public float? ReceivedQty { get; set; }
        //public float? IssueQty { get; set; }
    }
}
