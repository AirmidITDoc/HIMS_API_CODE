using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class IpbedTranferSmsqueryTempleteSm
    {
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public string? MobileNo { get; set; }
        public string? FromWardBedNo { get; set; }
        public string? Ipdno { get; set; }
        public string? ToWardBedNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? FromTime { get; set; }
        public string? TranDatetime { get; set; }
        public long? AdmissionId { get; set; }
        public long TransferId { get; set; }
        public string? DoctorMobileNo { get; set; }
        public string? DoctorName { get; set; }
        public long? FromWardId { get; set; }
        public long? ToWardId { get; set; }
        public string? TodaysDate { get; set; }
    }
}
