using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class IndentListbyIdDto
    {
        public long IndentId { get; set; }
        public string? IndentNo { get; set; }
        public string? IndentDate { get; set; }
        public string? IndentTime { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public string? Addedby { get; set; }
        public string? Comments { get; set; }
        public long? Isdeleted { get; set; }
        public bool? Isverify { get; set; }
        public bool? Isclosed { get; set; }
        public string? DIndentDate { get; set; }
        public string? DIndentTime { get; set; }
        public long? IsInchargeVerifyId { get; set; }
        public bool? Priority { get; set; }
    }
}
