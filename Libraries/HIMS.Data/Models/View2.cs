using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class View2
    {
        public long AdmissionId { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNo { get; set; }
        public string? AgeYear { get; set; }
        public string? Age { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? City { get; set; }
        public long? GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? PatientType { get; set; }
        public long? PrefixId { get; set; }
        public string? Expr1 { get; set; }
        public string? Expr2 { get; set; }
        public string? RoomName { get; set; }
        public int? RoomType { get; set; }
        public string? BedName { get; set; }
        public string? TariffName { get; set; }
        public string? DepartmentName { get; set; }
        public string? RelativeName { get; set; }
        public long RegId { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public int? Duration { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public string? SurgeryPart { get; set; }
        public DateTime? SurgeryFromTime { get; set; }
        public DateTime? SurgeryEndTime { get; set; }
        public int? SurgeryDuration { get; set; }
        public string? IsPrimary { get; set; }
        public long? Expr3 { get; set; }
        public string? Expr4 { get; set; }
        public string? Expr5 { get; set; }
        public long? Expr6 { get; set; }
        public string? Expr7 { get; set; }
        public string? Expr8 { get; set; }
        public string? DoctorType { get; set; }
        public long? DoctorId { get; set; }
        public string? Expr9 { get; set; }
        public long? Expr10 { get; set; }
        public string? Expr11 { get; set; }
        public long OtpreOperationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }
        public string? Expr12 { get; set; }
        public string? Expr13 { get; set; }
        public long VisitId { get; set; }
        public long? SurgeonId { get; set; }
        public long? AnesthetistId { get; set; }
        public long? DocNameId { get; set; }
    }
}
