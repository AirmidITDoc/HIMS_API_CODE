using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MisOpdocPatCnt
    {
        public string Label { get; set; } = null!;
        public int? Cnt { get; set; }
        public long? PatientOldNew { get; set; }
        public string? DoctorName { get; set; }
        public long? ConsultantDocId { get; set; }
    }
}
