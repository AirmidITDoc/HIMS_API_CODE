using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TariffMaster
    {
        public long TariffId { get; set; }
        public string? TariffName { get; set; }
        public bool? IsActive { get; set; }
    }
}
