using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MQuestionMaster
    {
        public long QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public bool? IsActive { get; set; }
        public string? ShortCutValues { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
