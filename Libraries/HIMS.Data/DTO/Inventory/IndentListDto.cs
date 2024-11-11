using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class IndentListDto
    {
        public string? IndentNo { get; set; }

        public long? ToStoreId { get; set; }
       
        public long? FromStoreId { get; set; }
        public string? FromStoreName { get; set; }
    }
}
