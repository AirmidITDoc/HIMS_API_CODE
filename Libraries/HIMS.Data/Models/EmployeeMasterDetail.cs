using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class EmployeeMasterDetail
    {
        public long EmployeeMasterDetId { get; set; }
        public long? Id { get; set; }
        public string? Code { get; set; }
        public long? PrefixId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public long? ReligionId { get; set; }
        public long? MotherTongueId { get; set; }
        public long? BloodGroupId { get; set; }
        public long? MaritalStatusId { get; set; }
        public DateTime? BirthDate { get; set; }
        public long? AgeYears { get; set; }
        public long? AgeMonth { get; set; }
        public long? AgeDays { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? DateofAnniversary { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Panno { get; set; }
        public string? Pfno { get; set; }
        public long? UnitId { get; set; }
        public long? DepartmentId { get; set; }
        public long? DesignationId { get; set; }
        public long? EmpCategoryId { get; set; }
        public long? EmpClassificationId { get; set; }
        public string? Passpotno { get; set; }
        public string? Esino { get; set; }
        public string? Laddress { get; set; }
        public long? LcountryId { get; set; }
        public long? LstateId { get; set; }
        public long? LdistrictId { get; set; }
        public long? LcityId { get; set; }
        public long? LareaId { get; set; }
        public string? Lpin { get; set; }
        public string? LcontactNo { get; set; }
        public string? Paddress { get; set; }
        public long? PcountryId { get; set; }
        public long? PstateId { get; set; }
        public long? PdistrictId { get; set; }
        public long? PcityId { get; set; }
        public long? PareaId { get; set; }
        public string? Ppin { get; set; }
        public string? PcontactNo { get; set; }
        public long? BankNameId { get; set; }
        public string? BankBranchName { get; set; }
        public string? BankAcNo { get; set; }
        public string? Micrno { get; set; }
        public string? AcType { get; set; }
        public DateTime? ResignationDt { get; set; }
        public string? EmpPhotoPath { get; set; }
        public string? EmpSinPath { get; set; }
        public bool? Status { get; set; }
        public DateTime? ApplicableFrom { get; set; }
        public bool? IsApproved { get; set; }
        public long? ApprovedBy { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string? TelNo { get; set; }
        public bool? TransApp { get; set; }
        public byte[]? EmpPhoto { get; set; }
        public byte[]? EmpSign { get; set; }
        public long? SubDepartmentId { get; set; }
        public long? WardId { get; set; }
    }
}
