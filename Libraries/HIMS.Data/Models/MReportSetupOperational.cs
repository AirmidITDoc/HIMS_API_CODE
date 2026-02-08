using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MReportSetupOperational
    {
        public long ReportId { get; set; }
        public long? UnitId { get; set; }
        public string? ReportMode { get; set; }
        public string? ReportHtmlName { get; set; }
        public string? ReportHeaderHtmlName { get; set; }
        public string? ProcedureName { get; set; }
        public string? FolderName { get; set; }
        public string? ReportFileName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
