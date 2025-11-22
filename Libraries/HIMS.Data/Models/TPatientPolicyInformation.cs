using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPatientPolicyInformation
    {
        public long PatientPolicyId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? PolicyNo { get; set; }
        public DateTime? PolicyValidateDate { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string? Comments { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
