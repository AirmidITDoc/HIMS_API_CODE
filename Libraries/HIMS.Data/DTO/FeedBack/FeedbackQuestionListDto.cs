using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.FeedBack
{
    public class FeedbackQuestionListDto
    {
        public long? DepartmentId { get; set; }
        public long FeedbackId { get; set; }
        public string? FeedbackQuestion { get; set; }
        public string? FeedbackQuestionMarathi { get; set; }
        public long? SequanceId { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DepartmentName { get; set; }


    }
    public class DepartmentWithFeedbackListDto
    {
        public long? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool? IsActive { get; set; }

    }
}
