using System;

namespace HIMS.Core.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class AuditLogAttribute : Attribute
    {
        public string Label { get; set; }
        public string ReferenceTable { get; set; }
        public string ReferenceColumn { get; set; }
        public bool IsEnum { get; set; }
        public bool Ignore { get; set; }
        public string BoolFor { get; set; }
        public string DateFormat { get; set; }
        public string ExportFormat { get; set; }
        public bool IsIgnoreTimeZone { get; set; }
    }
}
