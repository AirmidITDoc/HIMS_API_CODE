using HIMS.Data.Models;

namespace HIMS.Data.DTO.OPPatient
{
    //public partial class ServiceMasterList
    //{
    //    public decimal? ClassRate { get; set; }
    //    public long? TariffId { get; set; }
    //    public long? ClassId { get; set; }
    //}

    public class ServiceMasterDTO : ServiceMaster
    {
        public long ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public decimal ClassRate { get; set; }
        public decimal Price { get; set; }

        public long TariffId { get; set; }
        public long ClassId { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyServicePrint { get; set; }
        public bool? IsInclusionOrExclusion { get; set; }
        public string FormattedText { get { return this.ServiceName + " | Price : " + this.ClassRate.ToString("F2"); } }

    }
}
