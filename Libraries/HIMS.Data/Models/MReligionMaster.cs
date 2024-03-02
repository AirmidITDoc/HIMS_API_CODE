using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MReligionMaster
    {
        public long ReligionId { get; set; }
        public string? ReligionName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
