using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathSpecimenCollectionMaster
    {
        public long SpecimenCollectionId { get; set; }
        public string? CollectionMethod { get; set; }
        public long? UnitId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
