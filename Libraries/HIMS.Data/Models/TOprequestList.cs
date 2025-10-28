using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TOprequestList
    {
        public long RequestTranId { get; set; }
        public long? OpIpId { get; set; }
        public long? ServiceId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
