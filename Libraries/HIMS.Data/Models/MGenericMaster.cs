using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MGenericMaster
    {
        public long GenericId { get; set; }
        public string? GenericName { get; set; }
        public bool? IsActive { get; set; }
    }
}
