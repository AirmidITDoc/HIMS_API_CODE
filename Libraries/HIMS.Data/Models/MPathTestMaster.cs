using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathTestMaster
    {
        public long TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public bool? IsSubTest { get; set; }
        public string? TechniqueName { get; set; }
        public string? MachineName { get; set; }
        public string? SuggestionNote { get; set; }
        public string? FootNote { get; set; }
        public bool? Isdeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ServiceId { get; set; }
        public long? IsTemplateTest { get; set; }
    }
}
