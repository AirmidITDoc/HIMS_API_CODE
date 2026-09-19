using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TAbhaOnDiscover
    {
        public long Id { get; set; }
        public string TransactionId { get; set; } = null!;
        public string? PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? Gender { get; set; }
        public int? YearOfBirth { get; set; }
        public string? Mobile { get; set; }
        public string? AbhaNumber { get; set; }
        public string? AbhaAddress { get; set; }
        public string? UnverifiedIdentifiers { get; set; }
        public string? RawRequest { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
