using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MVillage
    {
        public int VillageId { get; set; }
        public string? VillageName { get; set; }
        public string? TalukaName { get; set; }
        public string? AddedByName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
