using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwServiceMasterList
    {
        public long ServiceId { get; set; }
        public long? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? ServiceShortDesc { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public bool? IsEditable { get; set; }
        public bool? CreditedtoDoctor { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public bool? IsActive { get; set; }
        public long? PrintOrder { get; set; }
        public long TariffId { get; set; }
        public string? TariffName { get; set; }
    }
}
