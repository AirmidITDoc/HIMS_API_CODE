using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class PurchaseRequisitionFinalHeaderListDto
    {
        public long Prid { get; set; }
        public string? Prno { get; set; }
        public DateTime? Prdate { get; set; }
        public DateTime? Prtime { get; set; }
        public string? UnitId { get; set; }
        public bool? Priority { get; set; }
        public string? StoreName { get; set; }
        public string? Comments { get; set; }
        public bool? Isclosed { get; set; }
        public bool? Isverify { get; set; }
        public string? IsVerifyBy { get; set; }
        public DateTime? IsVerifyDateTime { get; set; }
        public bool? IsCancelled { get; set; }
        public string? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class PurchaseRequisitionFinalDetailListDto
    {
        public long PrdetId { get; set; }
        public long Prid { get; set; }
        public string? FromStore{ get; set; }
        public string? ToStore { get; set; }
        public string? ItemName { get; set; }
        public double? Qty { get; set; }
        public long? PrrequestHeaderId { get; set; }
        public long? PrrequestDetId { get; set; }
    }

}
