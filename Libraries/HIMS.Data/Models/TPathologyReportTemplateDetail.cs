using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPathologyReportTemplateDetail
    {
        public long PathReportTemplateDetId { get; set; }
        public long? PathReportId { get; set; }
        public long? PathTemplateId { get; set; }
        public string? PathTemplateDetailsResult { get; set; }
        public long? TestId { get; set; }
    }
}
