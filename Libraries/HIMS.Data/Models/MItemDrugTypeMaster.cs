using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MItemDrugTypeMaster
    {
        public long ItemDrugTypeId { get; set; }
        public string? DrugTypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
