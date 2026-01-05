using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSubQuestionMaster
    {
        public long SubQuestionId { get; set; }
        public long? QuestionId { get; set; }
        public string? SubQuestionName { get; set; }
        public long? SequenceNo { get; set; }
        public string? ResultValues { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
