using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MHsncodeMaster
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
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
