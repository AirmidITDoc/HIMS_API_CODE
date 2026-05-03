using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemDrugTypeMaster
    {
        public MItemDrugTypeMaster()
        {
            MAssignItemToDrugs = new HashSet<MAssignItemToDrug>();
        }

        public long ItemDrugTypeId { get; set; }
        public string? DrugTypeName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MAssignItemToDrug> MAssignItemToDrugs { get; set; }
    }
}
