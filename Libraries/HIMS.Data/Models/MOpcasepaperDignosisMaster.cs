using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOpcasepaperDignosisMaster
    {
        public long Id { get; set; }
        public long? VisitId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
    }
}
