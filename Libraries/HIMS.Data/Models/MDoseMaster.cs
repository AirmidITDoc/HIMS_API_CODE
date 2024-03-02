using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoseMaster
    {
        public long DoseId { get; set; }
        public string? DoseName { get; set; }
        public string? DoseNameInEnglish { get; set; }
        public string? DoseNameInMarathi { get; set; }
        public bool? IsActive { get; set; }
        public double? DoseQtyPerDay { get; set; }
    }
}
