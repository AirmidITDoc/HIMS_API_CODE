﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class IndentItemListDto
    {

        public long? IndentId { get; set; }
        public long? IndentDetailsId { get; set; }
        public long? ItemId { get; set; }

        
        public string? ItemName { get; set; }
        public double? Qty { get; set; }
        public long? IndQty { get; set; }
        public long? IssQty { get; set; }

        public string? Comments { get; set; }

        public double? Bal { get; set; }

       

    }
}
