using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtAnaesthesiaClearance
    {
        public long AnaesthesiaClearanceId { get; set; }
        public long Opipid { get; set; }
        public byte Opiptype { get; set; }
        public long AnesthetistId { get; set; }
        public long? Asagrade { get; set; }
        public long? Airway { get; set; }
        public long AnaesthesiaTypeId { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Remarks { get; set; }
        public long ClearanceStatus { get; set; }
        public DateTime ClearanceDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
