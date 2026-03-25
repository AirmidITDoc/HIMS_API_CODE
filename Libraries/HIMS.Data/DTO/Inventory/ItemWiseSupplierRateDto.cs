using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class ItemWiseSupplierRateDto
    {
        public long DefId { get; set; }
        public long? ItemId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? SupplierRate { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ItemName { get; set; }
        public string? SupplierName { get; set; }

    }
}
