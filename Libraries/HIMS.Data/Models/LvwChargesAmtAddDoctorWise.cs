using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwChargesAmtAddDoctorWise
    {
        public double? AddChargesNetAmount { get; set; }
        public long BillNo { get; set; }
        public DateTime? DischargeDate { get; set; }
    }
}
