using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwCurrentAdmissionList
    {
        public long AdmissionId { get; set; }
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public byte? IsDischarged { get; set; }
        public byte? IsBillGenerated { get; set; }
    }
}
