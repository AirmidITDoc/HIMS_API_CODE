using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ServiceWiseCompanyModel
    {
        public long ServiceDetCompId { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyServicePrint { get; set; }
        public bool? IsInclusionOrExclusion { get; set; }
    }
}