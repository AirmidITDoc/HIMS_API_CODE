using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class MReportListDto
    {
        public long ReportId { get; set; }
        public string ReportSection { get; set; }
        public string ReportName { get; set; }
        public long? Parentid { get; set; }
        public string ReportMode { get; set; }
        public string ReportTitle { get; set; }
        public string ReportHeader { get; set; }
        public string ReportColumn { get; set; }
        public string ReportColumnWidth { get; set; }
        public string ReportColumnAligment { get; set; }
        public string ReportTotalField { get; set; }
        public string ReportGroupByLabel { get; set; }
        public string SummaryLabel { get; set; }
        public long SequenceNo { get; set; }
        public string ReportHeaderFile { get; set; }
        public string ReportBodyFile { get; set; }
        public string ReportFolderName { get; set; }
        public string ReportFileName { get; set; }
        public string ReportSpname { get; set; }
        public string ReportPageOrientation { get; set; }
        public string ReportPageSize { get; set; }
        public string ReportFilter { get; set; }
        public bool IsActive { get; set; }
        public long? MenuId { get; set; }
        public string ReportSummary { get; set; }
    }
}
