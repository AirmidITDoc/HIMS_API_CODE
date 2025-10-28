using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DoctorName
    {
        public long DoctorId { get; set; }
        public string? Doctorname1 { get; set; }
        public bool? IsRefDoc { get; set; }
        public bool? IsConsultant { get; set; }
        public long DoctorTypeId { get; set; }
        public bool? IsActive { get; set; }
        public string? DepartmentName { get; set; }
        public long? DepartmentId { get; set; }
    }
}
