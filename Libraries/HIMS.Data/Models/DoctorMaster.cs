﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class DoctorMaster
    {
        public DoctorMaster()
        {
            MDoctorChargesDetails = new HashSet<MDoctorChargesDetail>();
            MDoctorDepartmentDets = new HashSet<MDoctorDepartmentDet>();
            MDoctorExperienceDetails = new HashSet<MDoctorExperienceDetail>();
            MDoctorLeaveDetails = new HashSet<MDoctorLeaveDetail>();
            MDoctorQualificationDetails = new HashSet<MDoctorQualificationDetail>();
            MDoctorScheduleDetails = new HashSet<MDoctorScheduleDetail>();
            MDoctorSignPageDetails = new HashSet<MDoctorSignPageDetail>();
        }

        public long DoctorId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public long? GenderId { get; set; }
        public string? Education { get; set; }
        public bool? IsConsultant { get; set; }
        public bool? IsRefDoc { get; set; }
        public bool? IsActive { get; set; }
        public long DoctorTypeId { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? PassportNo { get; set; }
        public string? Esino { get; set; }
        public string? RegNo { get; set; }
        public DateTime? RegDate { get; set; }
        public string? MahRegNo { get; set; }
        public DateTime? MahRegDate { get; set; }
        public long? Addedby { get; set; }
        public long? UpdatedBy { get; set; }
        public string? RefDocHospitalName { get; set; }
        public bool? IsInHouseDoctor { get; set; }
        public bool? IsOnCallDoctor { get; set; }
        public string? PanCardNo { get; set; }
        public string? AadharCardNo { get; set; }
        public string? Signature { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MDoctorChargesDetail> MDoctorChargesDetails { get; set; }
        public virtual ICollection<MDoctorDepartmentDet> MDoctorDepartmentDets { get; set; }
        public virtual ICollection<MDoctorExperienceDetail> MDoctorExperienceDetails { get; set; }
        public virtual ICollection<MDoctorLeaveDetail> MDoctorLeaveDetails { get; set; }
        public virtual ICollection<MDoctorQualificationDetail> MDoctorQualificationDetails { get; set; }
        public virtual ICollection<MDoctorScheduleDetail> MDoctorScheduleDetails { get; set; }
        public virtual ICollection<MDoctorSignPageDetail> MDoctorSignPageDetails { get; set; }
    }
}
