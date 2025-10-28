using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOtcomplicationsMaster
    {
        public long ComplicationId { get; set; }
        public string? ComplicationDescr { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
