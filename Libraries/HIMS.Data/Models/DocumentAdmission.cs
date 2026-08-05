using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DocumentAdmission
    {
        public DocumentAdmission()
        {
            DocumentFiles = new HashSet<DocumentFile>();
        }

        public long Id { get; set; }
        public long AdmissionId { get; set; }
        public long RegId { get; set; }
        public bool IsActive { get; set; }
        public long IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DocNo { get; set; } = null!;

        public virtual Admission Admission { get; set; } = null!;
        public virtual Registration Reg { get; set; } = null!;
        public virtual ICollection<DocumentFile> DocumentFiles { get; set; }
    }
}
