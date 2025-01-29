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
        public int? IsActive { get; set; }
    }
}
