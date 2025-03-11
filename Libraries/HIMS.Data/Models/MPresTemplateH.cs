﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPresTemplateH
    {
        public MPresTemplateH()
        {
            MPresTemplateDs = new HashSet<MPresTemplateD>();
        }

        public long PresId { get; set; }
        public string? PresTemplateName { get; set; }
        public bool? IsActive { get; set; }
        public byte? OpIpType { get; set; }
        public long? IsAddBy { get; set; }
        public long? IsUpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MPresTemplateD> MPresTemplateDs { get; set; }
    }
}
