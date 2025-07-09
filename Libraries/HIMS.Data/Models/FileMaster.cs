using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class FileMaster
    {
        public long Id { get; set; }
        public long? RefId { get; set; }
        public long? RefType { get; set; }
        public string? DocName { get; set; }
        public string? DocSavedName { get; set; }
        public bool? IsDelete { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
