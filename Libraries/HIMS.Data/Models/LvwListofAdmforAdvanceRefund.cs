using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwListofAdmforAdvanceRefund
    {
        public long AdmissionId { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? DoctorName { get; set; }
        public byte? IsBillGenerated { get; set; }
        public byte? IsDischarged { get; set; }
        public long RegId { get; set; }
        public string? Ipdno { get; set; }
    }
}
