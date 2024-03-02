using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ReportConfigDetail
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string DisplayName { get; set; } = null!;
        public string FieldName { get; set; } = null!;
        public int AggFunction { get; set; }
        public bool IsShowFooter { get; set; }
        public bool IsShowHeader { get; set; }

        public virtual ReportConfigMaster Report { get; set; } = null!;
    }
}
