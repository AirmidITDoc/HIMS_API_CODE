using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPastHistoryMaster
    {
        public long PastHistoryId { get; set; }
        public string? PastHistoryDescr { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
