using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathParaRangeMaster
    {
        public long PathparaRangeId { get; set; }
        public long? ParaId { get; set; }
        public long? SexId { get; set; }
        public string? MinValue { get; set; }
        public string? Maxvalue { get; set; }
        public bool? IsDeleted { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
    }
}
