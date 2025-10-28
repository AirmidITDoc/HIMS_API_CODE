using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwPathParaFill
    {
        public long ParameterId { get; set; }
        public string? ParameterName { get; set; }
        public string? NormalRange { get; set; }
        public long? UnitId { get; set; }
        public string? UnitName { get; set; }
        public long? IsNumeric { get; set; }
    }
}
