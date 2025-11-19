using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPatientPolicyInformation
    {
        public long? PatientId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? PolicyNo { get; set; }
        public DateTime? PolicyValidateDate { get; set; }
        public decimal? ApprovedAmount { get; set; }
    }
}
