using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSiteDescriptionMaster
    {
        public long SiteDescId { get; set; }
        public string? SiteDescriptionName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? SurgeryCategoryId { get; set; }
    }
}
