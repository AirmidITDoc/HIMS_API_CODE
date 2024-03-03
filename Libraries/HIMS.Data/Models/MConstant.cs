using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MConstant
    {
        public long ConstantId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? ConstantType { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
