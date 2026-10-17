using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsCommanModeMapping
    {
        public long SysgetTableId { get; set; }
        public string? SysModeType { get; set; }
        public string? SysModeName { get; set; }
        public string? SysProcedureName { get; set; }
    }
}
