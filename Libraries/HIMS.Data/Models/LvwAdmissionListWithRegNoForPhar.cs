using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwAdmissionListWithRegNoForPhar
    {
        public string? RegNo { get; set; }
        public long AdmissionId { get; set; }
        public string? PatientName { get; set; }
        public string? Ipdno { get; set; }
        public string? AdmittedDr { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public long RegId { get; set; }
        public string? Doa { get; set; }
    }
}
