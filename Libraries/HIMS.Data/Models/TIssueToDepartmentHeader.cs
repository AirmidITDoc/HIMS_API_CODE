using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIssueToDepartmentHeader
    {
        public long IssueId { get; set; }
        public long? IssueNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? IssueTime { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public double? TotalAmount { get; set; }
        public double? TotalVatAmount { get; set; }
        public double? NetAmount { get; set; }
        public string? Remark { get; set; }
        public string? Receivedby { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsClosed { get; set; }
        public long? IndentId { get; set; }
    }
}
