using AutoMapper;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pharmacy;
using HIMS.Data.Models;

namespace HIMS.API.Infrastructure
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
           CreateMap<DbPrefixMaster, PrefixModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
           CreateMap<DbGenderMaster, GenderModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
           CreateMap<CashCounter, CashCounterModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesHeader, SalesModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TSalesDetail, SalesDetailModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();

        }
    }
}
