using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ModuleMaster
    {
        public int Id { get; set; }
        public string? ModuleName { get; set; }
        public bool? IsActive { get; set; }
    }
}
