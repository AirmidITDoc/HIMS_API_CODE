using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TReportLog
    {
        public long LogId { get; set; }
        public long? Opipid { get; set; }
        public long? Opiptype { get; set; }
        public long? LogTypeId { get; set; }
        public string? LogTypeName { get; set; }
        public DateTime? LogDate { get; set; }
        public DateTime? LogTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
