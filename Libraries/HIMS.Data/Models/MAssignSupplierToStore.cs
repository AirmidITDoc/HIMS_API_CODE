﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MAssignSupplierToStore
    {
        public long AssignId { get; set; }
        public long StoreId { get; set; }
        public long SupplierId { get; set; }

        public virtual MStoreMaster Store { get; set; } = null!;
        public virtual MSupplierMaster Supplier { get; set; } = null!;
    }
}
