using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MLoginAccessConfig
    {
        public long LoginConfigId { get; set; }
        public long? AccessCategoryId { get; set; }
        public string? AccessValueId { get; set; }
        public bool? IsInputField { get; set; }
    }
}
