using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class ClinicalQuesHeader
    {
        public long ClinicalQuesHeaderId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
