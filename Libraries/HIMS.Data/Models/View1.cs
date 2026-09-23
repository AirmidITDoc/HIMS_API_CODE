using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class View1
    {
        public DateTime? VisitDate { get; set; }
        public DateTime? VisitTime { get; set; }
        public long RegId { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Age { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? GenderName { get; set; }
        public string? Expr1 { get; set; }
        public string? Expr2 { get; set; }
        public string? Expr3 { get; set; }
    }
}
