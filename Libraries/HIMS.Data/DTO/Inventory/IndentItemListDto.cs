using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class IndentItemListDto
    {

        
        public long? IndentDetailsId { get; set; }
        public long? ItemId { get; set; }

        
        public string? ItemName { get; set; }
        
             public long? IndQty { get; set; }
        public long? IndTotalQty { get; set; }
        public long? IssQty { get; set; }

        public long? FromStoreId { get; set; }

        public long? Bal { get; set; }

        public long? IndentId { get; set; }
        public bool? IsClosed { get; set; }


    }
}
