using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Pathology
{
    public class HomeCollectionModel
    {
        public long HomeCollectionId { get; set; }
        public long? UnitId { get; set; }
        //public string? HomeSeqNo { get; set; }
        public long PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public DateTime? DateofBirth { get; set; }
        public long? AgeY { get; set; }
        public long? AgeM { get; set; }
        public long? AgeD { get; set; }
        public string? MobileNo { get; set; }
        public string? Address { get; set; }
        public long? PatRegId { get; set; }
        public string? Remark { get; set; }
        public bool? Priority { get; set; }
        public DateTime? CollectionDate { get; set; }
        public string? CollectionTime { get; set; }
        public long? Phlebotomist { get; set; }
        public string? Location { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Radius { get; set; }
        public List<HomeCollectionServiceDetailModel> THomeCollectionServiceDetails { get; set; }

    }
    public class HomeCollectionServiceDetailModel
    {
        public long HomeDetId { get; set; }
        public long? HomeCollectionId { get; set; }
        public long? UnitId { get; set; }
        public long? TestId { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public bool? IsCancel { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
    }
}
