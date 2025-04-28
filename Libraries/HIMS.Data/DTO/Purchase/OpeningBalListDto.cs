using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Purchase
{
    public class OpeningBalListDto
    {
        public long OpeningHId { get; set; }

        //public long? StoreId { get; set; }

        public String? StoreName { get; set; }
        public String? OpeningDate { get; set; }
        public String? AdddedByName { get; set; }
     

    }
}
