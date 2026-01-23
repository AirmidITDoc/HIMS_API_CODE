using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLabPatientPersonInfo
    {
        public long PatientInfoId { get; set; }
        public long? UnitId { get; set; }
        public long? PatientId { get; set; }
        public string? EmailIdOrMobileNo { get; set; }
        public string? Comments { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
