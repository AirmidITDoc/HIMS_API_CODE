using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPatIcdcdeD
    {
        public long Did { get; set; }
        public long? Hid { get; set; }
        public string? IcdCode { get; set; }
        public string? IcdCodeDesc { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? IcdcdeMainName { get; set; }
        public long? MainIcdcdeId { get; set; }
    }
}
