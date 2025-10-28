using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDiagnosisMaster
    {
        public long DiagnosisId { get; set; }
        public string? DiagnosisDescr { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
