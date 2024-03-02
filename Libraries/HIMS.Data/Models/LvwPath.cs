using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwPath
    {
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? Opdno { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? VisitTime { get; set; }
        public string? ConsultantDoctorName { get; set; }
        public long VisitId { get; set; }
        public long? PathTestId { get; set; }
        public string? ServiceName { get; set; }
        public long PathReportId { get; set; }
        public string? PbillNo { get; set; }
        public DateTime? PathDate { get; set; }
    }
}
