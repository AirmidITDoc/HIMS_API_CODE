using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MExpensesHeadMaster
    {
        public long ExpHedId { get; set; }
        public string? HeadName { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
