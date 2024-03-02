using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathTestFormula
    {
        public long FormulaId { get; set; }
        public long? ParameterId { get; set; }
        public string? ParameterName { get; set; }
        public string? Formula { get; set; }
    }
}
