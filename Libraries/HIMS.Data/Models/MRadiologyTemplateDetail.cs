﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MRadiologyTemplateDetail
    {
        public long PtemplateId { get; set; }
        public long? TestId { get; set; }
        public long? TemplateId { get; set; }

        public virtual MRadiologyTestMaster? Test { get; set; }
    }
}
