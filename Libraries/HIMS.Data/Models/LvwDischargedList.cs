using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwDischargedList
    {
        public long AdmissionId { get; set; }
        public long? RegId { get; set; }
        public long? PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public string? GenderName { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? Doa { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public string? Dot { get; set; }
        public long? PatientTypeId { get; set; }
        public string PatientType { get; set; } = null!;
        public long? HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? RoomName { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public string? BedName { get; set; }
        public long? DocNameId { get; set; }
        public long RefDocNameId { get; set; }
        public string? Doctorname { get; set; }
        public string RefDocName { get; set; } = null!;
        public byte? IsDischarged { get; set; }
        public byte? IsBillGenerated { get; set; }
    }
}
