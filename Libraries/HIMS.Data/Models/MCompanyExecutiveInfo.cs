using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCompanyExecutiveInfo
    {
        public long Id { get; set; }
        public long? CompanyId { get; set; }
        public long? EmployeId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual CompanyMaster? Company { get; set; }
    }
}
