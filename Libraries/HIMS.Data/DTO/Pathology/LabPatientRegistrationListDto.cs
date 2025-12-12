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
        public string? MobileNo { get; set; }
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
        public long TotalAmount { get; set; }
        public long DiscAmount { get; set; }
        public long NetAmount { get; set; }
        public long PaidAmount { get; set; }
        public string? PBillNo { get; set; }
        public long IsCancelled { get; set; }
        public decimal PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? OnlinePay { get; set; }
        //public int? PayCount { get; set; }
       
        //public string? CashCounterName { get; set; }
        public string? BillNo { get; set; }
        public string? companyName { get; set; }



    }
}
