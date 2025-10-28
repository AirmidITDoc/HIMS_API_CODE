using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TDrbillDet
    {
        public long DrbillDetId { get; set; }
        public long? Drno { get; set; }
        public long? ChargesId { get; set; }
    }
}
