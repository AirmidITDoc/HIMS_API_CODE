using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwOtdetailsDoctorWise
    {
        public string? PatientName { get; set; }
        public long RegId { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public long? Id { get; set; }
        public string? DoctorName { get; set; }
        public DateTime? OpDate { get; set; }
        public DateTime? Optime { get; set; }
        public string? ProcedureName { get; set; }
        public double? Amount { get; set; }
    }
}
