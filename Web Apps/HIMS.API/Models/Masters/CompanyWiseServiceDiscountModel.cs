namespace HIMS.API.Models.Masters
{
    public class CompanyWiseServiceDiscountModel
    {
        public bool? IsGroupOrSubGroup { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public double? DiscountPercentage { get; set; }
        public long? userId { get; set; }


    }
    public class CompanyWiseServiceModel
    {

        public List<CompanyWiseServiceDiscountModel> CompanyWiseService { get; set; }
        public long? userId { get; set; }



    }
}
