using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MExternalDoctorMaster
    {
        public long ExtDoctorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DoctorName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
