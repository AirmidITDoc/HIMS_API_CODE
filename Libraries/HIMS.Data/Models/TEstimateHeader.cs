using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TEstimateHeader
    {
        public TEstimateHeader()
        {
            TEstimateDetails = new HashSet<TEstimateDetail>();
        }

        public long EstimateId { get; set; }
        public long? UnitId { get; set; }
        public string? EstimateNo { get; set; }
        public long? PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public long? AgeYear { get; set; }
        public long? CityId { get; set; }
        public long? DoctorId { get; set; }
        public long? CompanyId { get; set; }
        public string? Comments { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TEstimateDetail> TEstimateDetails { get; set; }
    }
}
