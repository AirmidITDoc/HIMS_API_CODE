using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Marketing
{
    public class MarketingListDto
    {
        public long Id { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? VisitTime { get; set; }
        public long? MarketingPersonId { get; set; }
        public long? HospitalId { get; set; }
        public long? CityId { get; set; }
        public string? Location { get; set; }
        public string? CityName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Radius { get; set; }
        public long? FollowupType { get; set; }
        public string? FollowupTypeName { get; set; }
        public string? Comment { get; set; }
        public long? Status { get; set; }
        public string? StatusName { get; set; }
        public DateTime? ClosedDate { get; set; }
        public decimal? EstimatedValue { get; set; }
        public bool? IsLeadPriority { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? FollowTypeDate { get; set; }
        public bool? VerifyVisit { get; set; }
        public string? VisitCommentSummary { get; set; }

        
    }
    public class MarketingAppFollowVisitSummaryDto
    {
        public long Id { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? VisitTime { get; set; }
        public long? MarketingPersonId { get; set; }
        public long? HospitalId { get; set; }
        public long? CityId { get; set; }
        public string? Location { get; set; }
        public string? CityName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Radius { get; set; }
        public long? FollowupType { get; set; }
        public string? FollowupTypeName { get; set; }
        public string? Comment { get; set; }
        public long? Status { get; set; }
        public string? StatusName { get; set; }
        public DateTime? ClosedDate { get; set; }
        public decimal? EstimatedValue { get; set; }
        public bool? IsLeadPriority { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? FollowTypeDate { get; set; }
        public bool? VerifyVisit { get; set; }
        public string? VisitCommentSummary { get; set; }


    }


}
