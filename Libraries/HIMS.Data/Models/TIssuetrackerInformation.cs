using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TIssuetrackerInformation
    {
        public long IssueTrackerId { get; set; }
        public long? IssueTrackerNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? IssueTime { get; set; }
        public long? CustomerId { get; set; }
        public string? IssueName { get; set; }
        public string? IssueDescription { get; set; }
        public long? IssueTypeId { get; set; }
        public long? IssueRaisedId { get; set; }
        public long? IssueStatusId { get; set; }
        public long? IssueAssignedId { get; set; }
        public string? DeveloperComment { get; set; }
        public string? TesterComment { get; set; }
        public byte? IsCodeRelease { get; set; }
        public byte? IsReviewStatus { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public DateTime? ResolvedTime { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
