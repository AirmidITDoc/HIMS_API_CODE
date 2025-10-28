using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DbPurposeMaster
    {
        public long PurposeId { get; set; }
        public string? PurposeName { get; set; }
        public long? IsActive { get; set; }
    }
}
