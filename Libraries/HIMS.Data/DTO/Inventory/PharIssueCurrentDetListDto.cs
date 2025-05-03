using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class PharIssueCurrentDetListDto
    {

        public long IssueNo { get; set; }
        public DateTime IssueDate { get; set; }
        public long FromStoreId { get; set; }
        public string FromStoreName { get; set; }
        public long ToStoreId { get; set; }
        public string ToStoreName { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public double IssueQty { get; set; }
        public decimal LandedRate { get; set; }
        public decimal UnitMRP { get; set; }
        public decimal PurRate { get; set; }
        public long StkId { get; set; }
    }
}
