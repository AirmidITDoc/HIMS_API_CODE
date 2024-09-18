using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIpPrescriptionDischarge
    {
        public long PrecriptionId { get; set; }
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public long? InstructionId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Instruction { get; set; }
        public string? Remark { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsEnglishOrIsMarathi { get; set; }
        public long? StoreId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Admission? OpdIpd { get; set; }
    }
}
