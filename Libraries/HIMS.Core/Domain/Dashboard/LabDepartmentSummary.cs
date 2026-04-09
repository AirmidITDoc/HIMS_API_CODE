using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Domain.Dashboard
{
    public class LabDepartmentSummary
    {
        public string GroupName { get; set; }
        public string ServiceName { get; set; }
        public long TestCount { get; set; }
        public decimal CenterSale { get; set; }
        public decimal Corporate { get; set; }
        public decimal Digital { get; set; }
        public decimal Referral { get; set; }
        public decimal NetSale { get; set; }
        public long GroupId { get; set; }
    }
}
