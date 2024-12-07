using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DoctorTypeMaster
    {
        public long Id { get; set; }
        public string? DoctorType { get; set; }
        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
