using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MAreaMaster
    {
        public long AreaId { get; set; }
        public string? AreaName { get; set; }
        public long? TalukaId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
