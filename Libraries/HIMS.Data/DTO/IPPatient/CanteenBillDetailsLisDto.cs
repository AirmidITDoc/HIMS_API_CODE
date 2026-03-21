using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class CanteenBillDetailsLisDto
    {

        public string? BillNo { get; set; }

        public string? PBillNo { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? Date { get; set; }
        public decimal? UnitMrp { get; set; }
        public double? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        //public double? ItemGst { get; set; }
        //public decimal? ItemGstAmt { get; set; }
        public double? ItemDisc { get; set; }
        public decimal? ItemDiscAmt { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? LandedPrice { get; set; }
        public decimal? TotalLandedAmount { get; set; }



        //public double? ReturnQty { get; set; }
        //public long? ReqId { get; set; }
    }
}
