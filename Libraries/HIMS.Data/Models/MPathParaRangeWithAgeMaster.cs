using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathParaRangeWithAgeMaster
    {
        public long PathparaRangeId { get; set; }
        public long? ParaId { get; set; }
        public long? SexId { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? AgeType { get; set; }
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
        public bool? IsDeleted { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
    }
}
