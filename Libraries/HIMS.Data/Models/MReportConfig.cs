using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MReportConfig
    {
        public long ReportId { get; set; }
        public string? ReportSection { get; set; }
        public string? ReportName { get; set; }
        public long? Parentid { get; set; }
        public string? ReportMode { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportHeader { get; set; }
        public string? ReportColumn { get; set; }
        public string? ReportHeaderFile { get; set; }
        public string? ReportBodyFile { get; set; }
        public string? ReportFolderName { get; set; }
        public string? ReportFileName { get; set; }
        public string? ReportSpname { get; set; }
        public string? ReportPageOrientation { get; set; }
        public string? ReportPageSize { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdateBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
