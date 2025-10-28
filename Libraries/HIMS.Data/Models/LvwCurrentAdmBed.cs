using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwCurrentAdmBed
    {
        public string? RegNo { get; set; }
        public string? PatientName { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public string? DoctorName { get; set; }
        public byte? IsDischarged { get; set; }
        public long? DocNameId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
