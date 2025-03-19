using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TSupPayDet
    {
        public long SupTranId { get; set; }
        public long? SupPayId { get; set; }
        public long? SupGrnId { get; set; }
    }
}
