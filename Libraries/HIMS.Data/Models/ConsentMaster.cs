using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ConsentMaster
    {
        public long ConsentId { get; set; }
        public DateTime? ConsentDate { get; set; }
        public DateTime? ConsentTime { get; set; }
        public long? RefId { get; set; }
        public long? RefType { get; set; }
        public string? ConsentName { get; set; }
        public long? ConsentDepartment { get; set; }
        public string? ConsentText { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
