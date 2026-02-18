using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPurchaseRequisitionDetail
    {
        public long PurchaseRequisitionDetId { get; set; }
        public long PurchaseRequisitionId { get; set; }
        public long? ItemId { get; set; }
        public double? Qty { get; set; }
        public double? VerifiedQty { get; set; }
        public long? IndQty { get; set; }
        public long? IssQty { get; set; }
        public bool IsClosed { get; set; }

        public virtual TPurchaseRequisitionHeader PurchaseRequisition { get; set; } = null!;
    }
}
