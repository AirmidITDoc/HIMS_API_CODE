using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSubQuestionValuesMaster
    {
        public long SubQuestionValId { get; set; }
        public long? SubQuestionId { get; set; }
        public string? SubQuestionValName { get; set; }
        public long? SequenceNo { get; set; }
        public string? ShortcutValues { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual MSubQuestionMaster? SubQuestion { get; set; }
    }
}
