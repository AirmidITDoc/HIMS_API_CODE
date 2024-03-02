using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class AdmittedPatientBalanceAmount
    {
        public decimal? ChargesAmount { get; set; }
        public long? OpdIpdId { get; set; }
    }
}
