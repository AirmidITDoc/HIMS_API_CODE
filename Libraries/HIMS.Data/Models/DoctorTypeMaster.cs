using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DoctorTypeMaster
    {
        public long Id { get; set; }
        public string? DoctorType { get; set; }
        public long? IsActive { get; set; }
    }
}
