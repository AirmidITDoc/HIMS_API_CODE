using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOtBillTemplate
    {
        public long TemplateId { get; set; }
        public long ServiceId { get; set; }
        public double? Percentage { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
