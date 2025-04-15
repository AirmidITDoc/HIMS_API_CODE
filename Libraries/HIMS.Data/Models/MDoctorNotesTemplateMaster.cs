using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDoctorNotesTemplateMaster
    {
        public long DocNoteTempId { get; set; }
        public string? DocsTempName { get; set; }
        public string? TemplateDesc { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
