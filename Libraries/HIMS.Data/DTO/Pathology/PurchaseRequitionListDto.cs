using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public  class PurchaseRequitionListDto
    {
        public long PurchaseRequisitionId { get; set; }
        public string? PurchaseRequisitionNo { get; set; }
        public DateTime? PurchaseRequisitionDate { get; set; }
        public DateTime? PurchaseRequisitionTime { get; set; }
        public long? UnitId { get; set; }
        public bool? Priority { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public string? Comments { get; set; }
        public bool? Isclosed { get; set; }
        public bool? Isverify { get; set; }
        public bool? IsInchargeVerify { get; set; }
        public long? IsInchargeVerifyId { get; set; }
        public DateTime? IsInchargeVerifyDate { get; set; }
        public bool? IsActive { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public long? Addedby { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancelled { get; set; }

    }
}
