using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LoginManager
    {
        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsActive { get; set; }
        public long? RoleId { get; set; }
        public long? StoreId { get; set; }
        public bool? IsDoctorType { get; set; }
        public long? DoctorId { get; set; }
        public bool? IsPoverify { get; set; }
        public bool? IsGrnverify { get; set; }
        public bool? IsCollection { get; set; }
        public bool? IsBedStatus { get; set; }
        public bool? IsCurrentStk { get; set; }
        public bool? IsPatientInfo { get; set; }
        public bool? IsDateInterval { get; set; }
        public int? IsDateIntervalDays { get; set; }
        public string? MailId { get; set; }
        public string? MailDomain { get; set; }
        public bool? LoginStatus { get; set; }
        public bool? AddChargeIsDelete { get; set; }
        public bool? IsIndentVerify { get; set; }
        public bool? IsPoinchargeVerify { get; set; }
        public bool? IsRefDocEditOpt { get; set; }
        public bool? IsInchIndVfy { get; set; }
        public string? UserToken { get; set; }
    }
}
