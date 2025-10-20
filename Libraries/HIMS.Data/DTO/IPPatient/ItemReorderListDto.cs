using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class ItemReorderListDto
    {
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? BalQty { get; set; }
        public string? Packing { get; set; }
        public string? StripQty { get; set; }
        public string? IndentQty { get; set; }





    }
}
