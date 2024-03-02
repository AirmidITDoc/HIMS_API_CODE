using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDoctorShareGenerationLog
    {
        public long DocShareId { get; set; }
        public DateTime? DocGeneratedDate { get; set; }
        public DateTime? DocGeneratedTime { get; set; }
        public byte? PatientType { get; set; }
        public long? DoctorId { get; set; }
        public long? BillNo { get; set; }
        public long? PbillNo { get; set; }
        public string? Description { get; set; }
        public long? ChargesId { get; set; }
        public DateTime? BillDate { get; set; }
    }
}
