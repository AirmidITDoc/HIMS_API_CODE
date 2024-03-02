using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TFileLocation
    {
        public long Id { get; set; }
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
    }
}
