using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOtSpecialtyMaster
    {
        public long SpecialtyId { get; set; }
        public string? SpecialtyName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
