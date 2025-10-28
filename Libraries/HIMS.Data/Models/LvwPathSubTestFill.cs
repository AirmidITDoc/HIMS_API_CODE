using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwPathSubTestFill
    {
        public long? ParameterId { get; set; }
        public string? ParameterName { get; set; }
        public long TestId { get; set; }
        public string? TestName { get; set; }
    }
}
