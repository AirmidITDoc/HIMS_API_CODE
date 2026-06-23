using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class VwDoctormaster
    {
        public long DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? GenderName { get; set; }
        public bool? IsRefDoc { get; set; }
        public bool? IsConsultant { get; set; }
    }
}
