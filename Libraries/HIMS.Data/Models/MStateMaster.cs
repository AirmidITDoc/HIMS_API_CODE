using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MStateMaster
    {
        public long StateId { get; set; }
        public string? StateName { get; set; }
        public long? CountryId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
