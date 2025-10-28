using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPrescriptionInstructionMaster
    {
        public long InstructionId { get; set; }
        public string? InstructionName { get; set; }
        public bool? IsActive { get; set; }
        public string? InstructionHindi { get; set; }
    }
}
