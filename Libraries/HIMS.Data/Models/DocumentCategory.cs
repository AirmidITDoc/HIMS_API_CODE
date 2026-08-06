using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DocumentCategory
    {
        public DocumentCategory()
        {
            DocumentFiles = new HashSet<DocumentFile>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string DocCategory { get; set; } = null!;
        public string? Icon { get; set; }
        public int? SortOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
    }
}
