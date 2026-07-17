using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MHsncodeMaster
    {
        public int HsncodeId { get; set; }
        public string HsncodeName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
