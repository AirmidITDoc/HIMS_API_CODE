using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ReportConfigMaster
    {
        public ReportConfigMaster()
        {
            ReportConfigDetails = new HashSet<ReportConfigDetail>();
        }

        public int Id { get; set; }
        public string ReportName { get; set; } = null!;
        public string ReportCode { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ReportType { get; set; }

        public virtual ICollection<ReportConfigDetail> ReportConfigDetails { get; set; }
    }
}
