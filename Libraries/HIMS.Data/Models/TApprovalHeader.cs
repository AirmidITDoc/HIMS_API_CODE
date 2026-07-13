using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TApprovalHeader
    {
        public long ApprovalId { get; set; }
        public string ApprovalNo { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public long TranId { get; set; }
        public string TransactionType { get; set; } = null!;
        public byte ApprovalStatus { get; set; }
        public long AuthorizeBy { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public string? Comment { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
