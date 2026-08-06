using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DocumentFile
    {
        public long Id { get; set; }
        public long DocCatId { get; set; }
        public long DocAdmissionId { get; set; }
        public string OrgFileName { get; set; } = null!;
        public string SavedFileName { get; set; } = null!;
        public string? FileTags { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DocNo { get; set; } = null!;

        public virtual DocumentAdmission DocAdmission { get; set; } = null!;
        public virtual DocumentCategory DocCat { get; set; } = null!;
    }
}
