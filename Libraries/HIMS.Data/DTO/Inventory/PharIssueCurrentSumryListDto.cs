using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class PharIssueCurrentSumryListDto
    {

        public long FromStoreId { get; set; }
        public string FromStoreName { get; set; }
        public long ToStoreId { get; set; }
        public string ToStoreName { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }

        public string BatchNo { get; set; }

        public double ReceivedQty { get; set; }

    }
}
