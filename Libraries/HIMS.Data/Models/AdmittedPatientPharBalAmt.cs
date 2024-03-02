using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class AdmittedPatientPharBalAmt
    {
        public long? OpIpId { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillBalAmt { get; set; }
        public double AdvBalAmt { get; set; }
    }
}
