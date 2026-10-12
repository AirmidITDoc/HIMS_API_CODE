using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOtBillTemplateDetail
    {
        public long OtbtempDetId { get; set; }
        public long? OtbtempId { get; set; }
        public long ServiceId { get; set; }
        public double? Percentage { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public virtual MOtBillTemplateHeader? Otbtemp { get; set; }
    }
}
