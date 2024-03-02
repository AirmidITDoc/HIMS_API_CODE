using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMrdAdmFile
    {
        public long Id { get; set; }
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FilePathLocation { get; set; }
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public long? RegId { get; set; }
    }
}
