﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPrescription
    {
        public long PrecriptionId { get; set; }
        public long? OpdIpdIp { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public string? Instruction { get; set; }
        public string? Remark { get; set; }
        public long? DoseOption2 { get; set; }
        public long? DaysOption2 { get; set; }
        public long? DoseOption3 { get; set; }
        public long? DaysOption3 { get; set; }
        public long? InstructionId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsEnglishOrIsMarathi { get; set; }
        public string? ChiefComplaint { get; set; }
        public string? Diagnosis { get; set; }
        public string? Examination { get; set; }
        public string? Height { get; set; }
        public string? Pweight { get; set; }
        public string? Bmi { get; set; }
        public string? Bsl { get; set; }
        public string? SpO2 { get; set; }
        public string? Temp { get; set; }
        public string? Pulse { get; set; }
        public string? Bp { get; set; }
        public long? StoreId { get; set; }
        public long? PatientReferDocId { get; set; }
        public string? Advice { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
