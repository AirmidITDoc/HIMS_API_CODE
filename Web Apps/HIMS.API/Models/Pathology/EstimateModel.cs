using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Pathology
{
    public class EstimateModel
    {
        public long EstimateId { get; set; }
        public long? UnitId { get; set; }
        //public string? EstimateNo { get; set; }
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
        public List<EstimateDetailModel> TEstimateDetails { get; set; }

    }
    public class EstimateDetailModel
    {
        public long EstimateDetId { get; set; }
        public long? EstimateId { get; set; }
        public long? ServiceId { get; set; }
        public decimal? Price { get; set; }
        public long? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }

    }
}
