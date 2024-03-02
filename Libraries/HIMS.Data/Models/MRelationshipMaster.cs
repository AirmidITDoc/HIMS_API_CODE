using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MRelationshipMaster
    {
        public long RelationshipId { get; set; }
        public string? RelationshipName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
