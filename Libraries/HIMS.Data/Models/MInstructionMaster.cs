using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MInstructionMaster
    {
        public long InstructionId { get; set; }
        public string? InstructionDescription { get; set; }
        public string? InstructioninMarathi { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
