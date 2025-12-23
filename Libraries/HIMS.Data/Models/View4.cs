using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class View4
    {
        public DateTime? AnesthesiaDate { get; set; }
        public DateTime? AnesthesiaTime { get; set; }
        public string? AnesthesiaNo { get; set; }
        public DateTime? AnesthesiaStartDate { get; set; }
        public DateTime? AnesthesiaStartTime { get; set; }
        public DateTime? AnesthesiaEndDate { get; set; }
        public DateTime? AnesthesiaEndTime { get; set; }
        public DateTime? RecoveryStartDate { get; set; }
        public DateTime? RecoveryStartTime { get; set; }
        public DateTime? RecoveryEndDate { get; set; }
        public DateTime? RecoveryEndTime { get; set; }
        public long? AnesthesiaType { get; set; }
        public long? Opipid { get; set; }
        public long AnesthesiaId { get; set; }
        public string? DescriptionType { get; set; }
        public string? DescriptionName { get; set; }
        public long? RegId { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? RegTime { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Age { get; set; }
        public string? MobileNo { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? GenderName { get; set; }
        public string? PatientType { get; set; }
        public string? Expr1 { get; set; }
        public string? Expr2 { get; set; }
        public string? PrefixName { get; set; }
        public string? RoomName { get; set; }
        public string? BedName { get; set; }
        public string? Ipdno { get; set; }
        public long? TariffId { get; set; }
        public string? TariffName { get; set; }
        public string? DepartmentName { get; set; }
        public string? CompanyName { get; set; }
    }
}
