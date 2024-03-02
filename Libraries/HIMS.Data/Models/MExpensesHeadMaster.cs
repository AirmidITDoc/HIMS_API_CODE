using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MExpensesHeadMaster
    {
        public long ExpHedId { get; set; }
        public string? HeadName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
