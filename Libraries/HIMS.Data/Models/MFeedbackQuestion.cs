using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MFeedbackQuestion
    {
        public long FeedbackId { get; set; }
        public string? FeedbackQuestion { get; set; }
        public string? FeedbackQuestionMarathi { get; set; }
        public long? DepartmentId { get; set; }
        public long? SequanceId { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
