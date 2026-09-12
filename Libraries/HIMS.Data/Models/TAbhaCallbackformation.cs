using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAbhaCallbackformation
    {
        public long AbhaPatientInformationId { get; set; }
        public string? PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? SbxId { get; set; }
        public string? Gender { get; set; }
        public string? MobileNumber { get; set; }
        public string? Dob { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? CreatedDate { get; set; }
        public int? ClientId { get; set; }
        public string? AbhaNumber { get; set; }
        public string? Json { get; set; }
        public string? TransactionId { get; set; }
    }
}
