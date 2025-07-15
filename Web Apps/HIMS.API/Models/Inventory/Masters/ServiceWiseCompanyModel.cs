using FluentValidation;
using HIMS.API.Models.Administration;

namespace HIMS.API.Models.Masters
{
    public class ServiceWiseCompanyModel
    {
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyServicePrint { get; set; }
        public bool? IsInclusionOrExclusion { get; set; }
    

    }



    public class ServiceWiseModel
    {

        public List<ServiceWiseCompanyModel> ServiceWise { get; set; }
        public long? userId { get; set; }



    }
}