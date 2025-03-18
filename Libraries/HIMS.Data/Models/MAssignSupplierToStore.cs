using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MAssignSupplierToStore
    {
        public long AssignId { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
    }
}
