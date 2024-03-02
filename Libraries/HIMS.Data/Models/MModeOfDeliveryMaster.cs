using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MModeOfDeliveryMaster
    {
        public long Id { get; set; }
        public string? ModeOfDelivery { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpDatedBy { get; set; }
    }
}
