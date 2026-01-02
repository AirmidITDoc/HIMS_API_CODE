using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TTokenNumberWithDepartmentWise
    {
        public long TokenId { get; set; }
        public DateTime? VisitDate { get; set; }
        public long? VisitId { get; set; }
        public long? DepartmentId { get; set; }
        public long? TokenNo { get; set; }
        public bool? IsStatus { get; set; }
    }
}
