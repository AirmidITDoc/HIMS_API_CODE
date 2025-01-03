﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class RegistrationListDto
    {

        public long? RegId { get; set; }
        public DateTime? RegTime { get; set; }
        public long? PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public long? GenderId { get; set; }
        public string? GenderName { get; set; }
        
        public string? City { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }

        public long? CountryId { get; set; }
        public long? MaritalStatusId { get; set; }
        public long? ReligionId { get; set; }
        public long? AreaId { get; set; }
        public string? AadharCardNo { get; set; }
        public string? MobileNo { get; set; }
        public string? PhoneNo { get; set; }
        public bool? IsCharity { get; set; }

        public string? PinNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Age { get; set; }
        public string? PatientName { get; set; }
        public string? RDate { get; set; }
        public string? RegTimeDate { get; set; }
        public string? RegNo { get; set; }
        public string? RegNoWithPrefix { get; set; }

    }
}
