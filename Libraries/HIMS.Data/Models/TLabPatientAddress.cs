using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLabPatientAddress
    {
        public long? LabPatientRegId { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
    }
}
