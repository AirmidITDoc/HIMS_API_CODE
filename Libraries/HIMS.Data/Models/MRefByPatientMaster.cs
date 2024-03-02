using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MRefByPatientMaster
    {
        public long RefById { get; set; }
        public string? RefByName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
