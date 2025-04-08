using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MNursingTemplateMaster
    {
        public long NursingId { get; set; }
        public string? NursTempName { get; set; }
        public string? TemplateDesc { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
