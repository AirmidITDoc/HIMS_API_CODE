using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ClassMaster
    {
        public long ClassId { get; set; }
        public string? ClassName { get; set; }
        public bool? IsActive { get; set; }
        public double? ClassRate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
