using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPcpndprocessDetail
    {
        public long PcpndprocessDetId { get; set; }
        public long? PcpndtprocessId { get; set; }
        public string? IndicationDesc { get; set; }
        public bool? IndicationValues { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual TPcpndprocess? Pcpndtprocess { get; set; }
    }
}
