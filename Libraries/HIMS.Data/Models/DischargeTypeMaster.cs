using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DischargeTypeMaster
    {
        public long DischargeTypeId { get; set; }
        public string? DischargeTypeName { get; set; }
        public bool? IsActive { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
