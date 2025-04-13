using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class IssuetodeptListDto
    {
        public long? ToStoreId { get; set; }
        public long? StoreId { get; set; }

        public string? FromStoreName { get; set; }
        public long? IssueId { get; set; }

        public string? IssueNo { get; set; }
        public string? IssueDate { get; set; }

        public string? IssueTime { get; set; }
        public long? TotalAmount { get; set; }


        public long? TotalVatAmount { get; set; }
        public long? NetAmount { get; set; }

        public long? Remark { get; set; }
        public string? Receivedby { get; set; }

        public long? Addedby { get; set; }
        public bool? IsVerified { get; set; }

        public bool? IsClosed { get; set; }
        public string? ToStoreName { get; set; }

        public string? IDate { get; set; }

        public long? IndentId { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
