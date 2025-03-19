using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathTestDetailMaster
    {
        public long TestDetId { get; set; }
        public long? TestId { get; set; }
        public long? ParameterId { get; set; }
        public long? SubTestId { get; set; }
    }
}
