using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCertificateTemplateMaster
    {
        public long TemplateId { get; set; }
        public string? Template { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
        public bool? Isdeleted { get; set; }
    }
}
