using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOpinvAdviceList
    {
        public long InvestigationAdvId { get; set; }
        public long? VisitId { get; set; }
        public long? ServiceId { get; set; }
        public string? Comments { get; set; }
    }
}
