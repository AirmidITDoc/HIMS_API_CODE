using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MAssignItemToStore
    {
        public long AssignId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
    }
}
