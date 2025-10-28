using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwVisitDetailsList
    {
        public long VisitId { get; set; }
        public long? RegId { get; set; }
        public string? PatientName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? DvisitDate { get; set; }
        public string? VisitTime { get; set; }
        public long DoctorId { get; set; }
        public string? Doctorname { get; set; }
        public long RefDocId { get; set; }
        public string RefDocName { get; set; } = null!;
        public long HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public long? PatientTypeId { get; set; }
        public string PatientType { get; set; } = null!;
        public string? VistDateTime { get; set; }
    }
}
