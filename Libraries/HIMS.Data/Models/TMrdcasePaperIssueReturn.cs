using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMrdcasePaperIssueReturn
    {
        public long MrdissueId { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? Issuetime { get; set; }
        public string? CasePaperIssueName { get; set; }
        public long? Designation { get; set; }
        public string? Reason { get; set; }
        public long? IssueUserId { get; set; }
        public bool? IsStatus { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? ReturnTime { get; set; }
        public string? ReturnByPersonName { get; set; }
        public long? ReturnUserId { get; set; }
    }
}
