﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TLabPatientRegistration
    {
        public TLabPatientRegistration()
        {
            TLabTestRequests = new HashSet<TLabTestRequest>();
        }

        public long LabPatientId { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? RegTime { get; set; }
        public long? UnitId { get; set; }
        public string? LabRequestNo { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? MobileNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public long? DepartmentId { get; set; }
        public long? DoctorId { get; set; }
        public long? RefDocId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TLabTestRequest> TLabTestRequests { get; set; }
    }
}
