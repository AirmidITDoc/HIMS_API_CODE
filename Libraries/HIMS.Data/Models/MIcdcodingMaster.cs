using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MIcdcodingMaster
    {
        public long IcdcodingId { get; set; }
        public string? Icdcode { get; set; }
        public string? IcdcodeName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? MainIcdcdeId { get; set; }
    }
}
