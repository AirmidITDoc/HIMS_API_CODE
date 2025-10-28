using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MReportConfiguration
    {
        public long ReportId { get; set; }
        public string? ReportSection { get; set; }
        public string? ReportName { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdateBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? Parentid { get; set; }
    }
}
