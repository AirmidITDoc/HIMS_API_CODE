using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class IndentListDto
    {
        public long? IndentId { get; set; }
        public string? IndentNo { get; set; }
        public DateTime? IndentDate { get; set; }
        public DateTime? IndentTime { get; set; }
        public long? FromStoreId { get; set; }
        public string? FromStoreName { get; set; }
        public long? ToStoreId { get; set; }
        public string? ToStoreName { get; set; }
        public string? Addedby { get; set; }
        public long? Isdeleted { get; set; }
        public bool? Isverify { get; set; }
        public bool? Isclosed { get; set; }
        public string? DIndentDate { get; set; }
        public string? DIndentTime { get; set; }
        public bool? IsInchargeVerify { get; set; }

    }
    public class IndentDetailListDto
    {
        public long IndentDetailsId { get; set; }
        public long IndentId { get; set; }
        public string ItemName { get; set; }
        public double Qty { get; set; }
        public double IndQty { get; set; }
        public double IssQty { get; set; }
        public double BalanceQty { get; set; }

    }
}
