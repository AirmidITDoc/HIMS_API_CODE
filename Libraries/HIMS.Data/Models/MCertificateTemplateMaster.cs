using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCertificateTemplateMaster
    {
        public long TemplateId { get; set; }
        public string? TemplateDesc { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
