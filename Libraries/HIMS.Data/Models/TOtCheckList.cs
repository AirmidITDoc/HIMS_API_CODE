using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOtCheckList
    {
        public long OtcheckListId { get; set; }
        public long Opipid { get; set; }
        public byte Opiptype { get; set; }
        public long CheckListDescriptionId { get; set; }
        public string CheckListDescription { get; set; } = null!;
        public bool Status { get; set; }
        public string? Remark { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
