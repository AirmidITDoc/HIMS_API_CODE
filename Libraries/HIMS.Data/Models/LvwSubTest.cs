using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwSubTest
    {
        public long TestId { get; set; }
        public string? TestName { get; set; }
        public long? SubTestId { get; set; }
        public long ParameterId { get; set; }
        public string? ParameterName { get; set; }
        public bool? IsSubTest { get; set; }
    }
}
