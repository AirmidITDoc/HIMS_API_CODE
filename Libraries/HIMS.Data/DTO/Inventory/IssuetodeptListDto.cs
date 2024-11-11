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
        public string? IssueNo { get; set; }

        public long? FromStoreId { get; set; }
        public string? FromStoreName { get; set; }

    }
}
