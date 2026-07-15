using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPatientAbhaInformation
    {
        public long AbhaTranId { get; set; }
        public long RegId { get; set; }
        public long AbhaNumber { get; set; }
        public string AbhaFullName { get; set; } = null!;
        public string AbhaAddress { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime YearOfBirth { get; set; }
        public bool Verified { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? VerifiedDateTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
