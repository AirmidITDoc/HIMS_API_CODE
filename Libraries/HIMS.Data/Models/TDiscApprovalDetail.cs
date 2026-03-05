using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDiscApprovalDetail
    {
        public long DiscSeqId { get; set; }
        public long Opipid { get; set; }
        public byte Opiptype { get; set; }
        public string? DiscApprovalNo { get; set; }
        public long BillNo { get; set; }
        public decimal RequestAmount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public long? AppovedBy { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public string? Comments { get; set; }
        public bool? IsActive { get; set; }
    }
}
