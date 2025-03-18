using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCountryMaster
    {
        public long CountryId { get; set; }
        public string? CountryName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
