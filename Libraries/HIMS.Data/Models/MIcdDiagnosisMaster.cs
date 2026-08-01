using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MIcdDiagnosisMaster
    {
        public int Icdid { get; set; }
        public string Icdversion { get; set; } = null!;
        public string Icdcode { get; set; } = null!;
        public string DiagnosisName { get; set; } = null!;
        public string? ShortName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
