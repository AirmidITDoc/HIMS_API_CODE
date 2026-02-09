using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public  class homeCollectionDetListDto
    {
        public long HomeCollectionId { get; set; }
        public long? UnitId { get; set; }
        public string? ServiceName { get; set; }
        public long? TestId { get; set; }
        public decimal Price { get; set; }
        public long Qty { get; set; }
        public decimal TotalAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal DiscAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
