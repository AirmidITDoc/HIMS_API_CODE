using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOtBillTemplateHeader
    {
        public MOtBillTemplateHeader()
        {
            MOtBillTemplateDetails = new HashSet<MOtBillTemplateDetail>();
        }

        public long OtbtempId { get; set; }
        public string OtBillTemplateName { get; set; } = null!;
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public virtual ICollection<MOtBillTemplateDetail> MOtBillTemplateDetails { get; set; }
    }
}
