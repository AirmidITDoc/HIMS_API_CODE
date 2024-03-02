using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPresTemplateD
    {
        public long PresDetId { get; set; }
        public string? PresId { get; set; }
        public DateTime? Date { get; set; }
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
        public bool? IsEnglishOrIsMarathi { get; set; }
    }
}
