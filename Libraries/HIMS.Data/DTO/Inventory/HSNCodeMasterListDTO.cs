using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class HSNCodeMasterListDTO
    {
        public long HsncodeId { get; set; }
        public string HsncodeName { get; set; } = null!;
        public double GstRate { get; set; }
        public long? UnitOfMeasureId { get; set; }
        public long? GstId { get; set; }
        public bool? IsActive { get; set; }
        public string UnitOfMeasure { get; set; } = null!;
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
    }
}
