using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MMessageTemplate
    {
        public long MsgId { get; set; }
        public string? Msg { get; set; }
        public long? PatientType { get; set; }
        public string? MsgCategory { get; set; }
        public string? Dbname { get; set; }
        public bool? IsBlock { get; set; }
        public string? TemplateId { get; set; }
    }
}
