using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemGenericNameMaster
    {
        public long ItemGenericNameId { get; set; }
        public string? ItemGenericName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
