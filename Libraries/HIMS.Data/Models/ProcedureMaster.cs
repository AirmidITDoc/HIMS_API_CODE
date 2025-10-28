using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ProcedureMaster
    {
        public long Id { get; set; }
        public string? ProcedureName { get; set; }
        public long? IsActive { get; set; }
    }
}
