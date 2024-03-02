using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSmsmappingTemplate
    {
        public long TMappingId { get; set; }
        public byte? PatientType { get; set; }
        public string? UserDefName { get; set; }
    }
}
