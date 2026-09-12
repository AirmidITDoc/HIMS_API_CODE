using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MFeedingRouteMaster
    {
        public long FeedingRouteId { get; set; }
        public string FeedingRouteCode { get; set; } = null!;
        public string FeedingRouteName { get; set; } = null!;
        public string? Description { get; set; }
        public long? DietTypesId { get; set; }
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
