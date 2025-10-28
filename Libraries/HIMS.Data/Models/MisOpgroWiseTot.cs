using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MisOpgroWiseTot
    {
        public string Label { get; set; } = null!;
        public string? DoctorName { get; set; }
        public byte? OpdIpdType { get; set; }
        public string? GroupNameS { get; set; }
        public long? PatientOldNew { get; set; }
        public double? ChargesTotalAmt { get; set; }
        public decimal? ChargesConAmt { get; set; }
        public decimal? ChargeNetAmt { get; set; }
        public long? ConsultantDocId { get; set; }
        public int? ServiceCnt { get; set; }
    }
}
