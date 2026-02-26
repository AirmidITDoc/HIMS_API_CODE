using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HIMS.Data.DTO.Pathology
{
    public  class PurchaseRequitionListDto
    {
        public long PurchaseRequisitionId { get; set; }
        public string? PurchaseRequisitionNo { get; set; }
        public DateTime? PurchaseRequisitionDate { get; set; }
        public DateTime? PurchaseRequisitionTime { get; set; }
        public string? HospitalName { get; set; }
        public bool? Priority { get; set; }
        public string? FromStore { get; set; }
        public string? ToStore { get; set; }
        public string? Comments { get; set; }
        public bool? Isclosed { get; set; }
        public bool? Isverify { get; set; }
        public bool? IsInchargeVerify { get; set; }
        public string? IsInchargeVerifyName { get; set; }
        public DateTime? IsInchargeVerifyDate { get; set; }
        public bool? IsActive { get; set; }
        public string? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public string? Addedby { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancelled { get; set; }


    }
    public class PurchaseRequisitionDetailListDto
    {
        public long PurchaseRequisitionDetId { get; set; }
        public long PurchaseRequisitionId { get; set; }
        public string? ItemName { get; set; }
        public double? Qty { get; set; }
        public double? VerifiedQty { get; set; }
        public long? IndQty { get; set; }
        public long? IssQty { get; set; }
        public bool IsClosed { get; set; }


    }
}
