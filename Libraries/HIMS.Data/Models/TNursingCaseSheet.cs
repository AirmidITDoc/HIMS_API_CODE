using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TNursingCaseSheet
    {
        public long NursingCaseSheetId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Mdate { get; set; }
        public DateTime? Mtime { get; set; }
        public string? Diagnosis { get; set; }
        public string? Operation { get; set; }
        public string? Pulse { get; set; }
        public string? Bp { get; set; }
        public string? Ecg { get; set; }
        public string? FitnessOf { get; set; }
        public string? ArrangeBlood { get; set; }
        public string? ReferToDr { get; set; }
        public string? DrugsReceived { get; set; }
        public string? NurseName { get; set; }
        public long? IsAddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public string? FitnessOf1 { get; set; }
        public string? ArrangeBlood1 { get; set; }
    }
}
