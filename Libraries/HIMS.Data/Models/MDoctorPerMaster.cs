using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoctorPerMaster
    {
        public long DoctorShareId { get; set; }
        public long? DoctorId { get; set; }
        public long? ServiceId { get; set; }
        public byte? DocShrType { get; set; }
        public string? DocShrTypeS { get; set; }
        public decimal? ServicePercentage { get; set; }
        public decimal? ServiceAmount { get; set; }
        public long? ClassId { get; set; }
        public long? ShrTypeSerOrGrp { get; set; }
        public byte? OpIpType { get; set; }
    }
}
