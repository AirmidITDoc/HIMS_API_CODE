using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathTemplateDetail
    {
        public long PtemplateId { get; set; }
        public long? TestId { get; set; }
        public long? TemplateId { get; set; }
    }
}
