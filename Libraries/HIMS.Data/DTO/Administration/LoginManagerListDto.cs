using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public  class LoginManagerListDto
    {
        public long UserId { get; set; }     
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public string? UserLoginName { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public string? RoleName { get; set; }
        public long RoleId { get; set; }
        public string? UserName { get; set; }
        public long StoreId { get; set; }
        public string? StoreName { get; set; }
        public bool? IsDoctorType { get; set; }
        public long DoctorID { get; set; }
        public string? DoctorName { get; set; }
        public bool? IsPoverify { get; set; }
        public bool? IsGrnverify { get; set; }
        public bool? IsCollection { get; set; }
        public bool? IsBedStatus { get; set; }
        public bool? IsCurrentStk { get; set; }
        public bool? IsPatientInfo { get; set; }
        public bool? IsDateInterval { get; set; }
        public int? IsDateIntervalDays { get; set; }
        public string MailId { get; set; } = null!;
        public string? MailDomain { get; set; }
        public bool? AddChargeIsDelete { get; set; }
        public bool? IsIndentVerify { get; set; }
        public bool? IsInchIndVfy { get; set; }
    }
}
