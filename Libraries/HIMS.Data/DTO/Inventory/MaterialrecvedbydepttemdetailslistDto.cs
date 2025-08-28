using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class MaterialrecvedbydepttemdetailslistDto
    {
        public long IssueDepId { get; set; }
        public long IssueId { get; set; }
        
        public String? ItemName { get; set; }

        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public double? IssueQty { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public double? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? LandedTotalAmount { get; set; }

        public long? StkId { get; set; }

        public long? StoreId { get; set; }
        public string? Status { get; set; }

    }
}
