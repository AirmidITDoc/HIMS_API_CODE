using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class CasePaper
    {
        public long CasePaperId { get; set; }
        public long? VisitId { get; set; }
        public string? Height { get; set; }
        public string? Weight { get; set; }
        public string? PastHistory { get; set; }
        public string? PresentHistory { get; set; }
        public string? Complaint { get; set; }
        public string? Finding { get; set; }
        public string? Diagnosis { get; set; }
        public string? Investigations { get; set; }
        public string? Bp { get; set; }
        public string? Pluse { get; set; }
        public string? Bsl { get; set; }
        public string? SpO2 { get; set; }
        public string? PersonalDetails { get; set; }
        public string? Temp { get; set; }
        public string? Bsa { get; set; }
        public string? Rr { get; set; }
        public string? Hc { get; set; }
        public long? SecondDocRef { get; set; }
        public string? Bmi { get; set; }
    }
}
