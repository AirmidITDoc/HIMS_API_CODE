using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MIcdDiagnosisMaster
    {
        public int Icdid { get; set; }
        public string Icdversion { get; set; } = null!;
        public string? ShortName { get; set; }
        public string Icdcode { get; set; } = null!;
        public string DiagnosisName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
