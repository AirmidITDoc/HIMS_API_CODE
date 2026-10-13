using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAbhaFileUpload
    {
        public long FileId { get; set; }
        public string? RegNo { get; set; }
        public string? Opipno { get; set; }
        public long? Opiptype { get; set; }
        public string? FileName { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
