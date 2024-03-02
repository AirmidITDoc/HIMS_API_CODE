using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwVisitDetailsForCasePaper
    {
        public long VisitId { get; set; }
        public string? VisitDate { get; set; }
        public DateTime? VisitTime { get; set; }
        public string? DocName { get; set; }
        public string? PatientName { get; set; }
        public string Height { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public string Pluse { get; set; } = null!;
        public string Bp { get; set; } = null!;
        public string PastHistory { get; set; } = null!;
        public string PresentHistory { get; set; } = null!;
        public string Complaint { get; set; } = null!;
        public string Finding { get; set; } = null!;
        public string Diagnosis { get; set; } = null!;
        public string Investigations { get; set; } = null!;
        public string PersonalDetails { get; set; } = null!;
        public long CasePaperId { get; set; }
        public string? RegNo { get; set; }
        public string Bsl { get; set; } = null!;
        public string SpO2 { get; set; } = null!;
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? Age { get; set; }
        public long? SexId { get; set; }
        public string? GenderName { get; set; }
        public string? Opdno { get; set; }
        public string Temp { get; set; } = null!;
        public string Bsa { get; set; } = null!;
        public string? Education { get; set; }
        public string? MahRegNo { get; set; }
        public long RegId { get; set; }
        public string? DepartmentName { get; set; }
        public string Hc { get; set; } = null!;
        public string Rr { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long SecondDocRef { get; set; }
    }
}
