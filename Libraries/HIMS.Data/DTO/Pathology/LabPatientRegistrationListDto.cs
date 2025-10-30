using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class LabPatientRegistrationListDto
    {
        public long LabPatientId { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? RegTime { get; set; }
        public string? LabRequestNo { get; set; }
        public string? PatientName { get; set; }
        public string? GenderName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? Address { get; set; }
        public string? CityName { get; set; }
        public string? DoctorName { get; set; }
        public string? RefDoctorName { get; set; }
        public string? DepartmentName { get; set; }
        public string? UserName { get; set; }
        public string? HospitalName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }


    }
}
