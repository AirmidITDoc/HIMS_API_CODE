using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ClinicalQuesDetail
    {
        public long ClinicalQuesDetId { get; set; }
        public long? ClinicalQuesHeaderId { get; set; }
        public long? SubQuesId { get; set; }
        public string? SubQuesName { get; set; }
        public string? ResultEntry { get; set; }
        public long? SeqNo { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
