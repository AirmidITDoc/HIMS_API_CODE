using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Tpr
    {
        public long Tprid { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string? Temperature { get; set; }
        public string? Pulse { get; set; }
        public string? Respiration { get; set; }
        public string? Bp { get; set; }
        public long? TakenBy { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public short? Avpu { get; set; }
        public int? MewsScore { get; set; }
        public bool? IsSynchronised { get; set; }
    }
}
