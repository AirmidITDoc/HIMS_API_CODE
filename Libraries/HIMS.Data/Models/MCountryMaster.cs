using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCountryMaster
    {
        public long CountryId { get; set; }
        public string? CountryName { get; set; }
        public long? AddBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
