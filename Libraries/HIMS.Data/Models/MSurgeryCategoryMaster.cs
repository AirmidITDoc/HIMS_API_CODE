using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSurgeryCategoryMaster
    {
        public long SurgeryCategoryId { get; set; }
        public string? SurgeryCategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
