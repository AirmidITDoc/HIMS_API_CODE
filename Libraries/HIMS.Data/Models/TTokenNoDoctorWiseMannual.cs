using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TTokenNoDoctorWiseMannual
    {
        public long TokenId { get; set; }
        public DateTime? VisitDate { get; set; }
        public long? VisitId { get; set; }
        public long? RegId { get; set; }
        public long? DoctorId { get; set; }
        public long? TokenNo { get; set; }
        public bool? IsStatus { get; set; }
        public bool? IsCancelled { get; set; }
    }
}
