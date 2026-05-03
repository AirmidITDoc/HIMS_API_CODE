using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MAssignItemToDrug
    {
        public long AssignId { get; set; }
        public long? DrugId { get; set; }
        public long? ItemId { get; set; }

        public virtual MItemDrugTypeMaster? Drug { get; set; }
        public virtual MItemMaster? Item { get; set; }
    }
}
