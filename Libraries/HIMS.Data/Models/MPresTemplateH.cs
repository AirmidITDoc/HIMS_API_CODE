using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPresTemplateH
    {
        public long PresId { get; set; }
        public string? PresTemplateName { get; set; }
        public bool? IsActive { get; set; }
        public byte? OpIpType { get; set; }
        public long? IsAddBy { get; set; }
        public long? IsUpdatedBy { get; set; }
    }
}
