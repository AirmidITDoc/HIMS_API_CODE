using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MModeOfPayment
    {
        public long Id { get; set; }
        public string? ModeOfPayment { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
