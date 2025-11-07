using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pharmacy
{
    public class ItemGenericByNameListDto
    {
        public long ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemGenericName { get; set; }

        public long? ItemGenericNameId { get; set; }

    }
}
