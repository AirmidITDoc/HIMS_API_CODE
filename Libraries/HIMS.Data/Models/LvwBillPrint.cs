using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwBillPrint
    {
        public long BillNo { get; set; }
        public long? ChargesId { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long RegId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public long? GenderId { get; set; }
        public string? DocName { get; set; }
        public long? DoctorId { get; set; }
        public double? DocPercentage { get; set; }
        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
        public DateTime? ChargesDate { get; set; }
        public bool? IsGenerated { get; set; }
        public bool? IsCancelled { get; set; }
        public decimal? BillAmt { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalAmt { get; set; }
        public DateTime? BillDate { get; set; }
    }
}
