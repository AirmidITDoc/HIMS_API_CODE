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
        public string? StoreId { get; set; }
        public string? StoreName { get; set; }
        public DateTime? OpeningDate { get; set; }
        public string? AdddedByName { get; set; }
        public string? OpeningDocNo { get; set; }
        public string? OpeningTime { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
