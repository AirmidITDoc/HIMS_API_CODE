using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPharPatientInformation
    {
        public long PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientAddress { get; set; }
        public long? MobileNo { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
    }
}
