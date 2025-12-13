using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SmspdfConfig
    {
        public long Smsid { get; set; }
        public string? Type { get; set; }
        public string? PdfModeName { get; set; }
        public string? FieldName { get; set; }
        public bool? PasswordProtectedPdf { get; set; }
    }
}
