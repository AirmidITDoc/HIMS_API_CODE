using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.AbhaIntegration
{

    public class PatientVisitRequest
    {
        public string AbhaNumber { get; set; }
        public string AbhaAddress { get; set; }
        public string HipId { get; set; }
        public string OpIpId { get; set; }
        public string OpIpType { get; set; }
    }

    public class PatientVisitResponse
    {
        public Patient Patient { get; set; }
        public List<Visit> Visits { get; set; }
        public string HipId { get; set; }
    }

    public class Patient
    {
        public string PatientRegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string HealthId { get; set; }
        public string HealthIdNumber { get; set; }
        public string DayOfBirth { get; set; }
        public string MonthOfBirth { get; set; }
        public string YearOfBirth { get; set; }
        public string HipId { get; set; }
    }

    public class Visit
    {
        public string VisitNumber { get; set; }
        public string VisitReason { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string VisitType { get; set; }
        public EncounterCode EncounterCode { get; set; }
    }

    public class Doctor
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Prefix { get; set; }
        public string Designation { get; set; }
        public string Degree { get; set; }
        public string Speciality { get; set; }
    }

    public class EncounterCode
    {
        public string Text { get; set; }
        public EncounterCodeDetails Code { get; set; }
    }

    public class EncounterCodeDetails
    {
        public string HospitalId { get; set; }
        public string Category { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
        public string Display { get; set; }
    }

}
