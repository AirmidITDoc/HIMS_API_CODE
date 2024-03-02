using AutoMapper;
using HIMS.API.Models.Masters;
using HIMS.Data.Models;

namespace HIMS.API.Infrastructure
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
           CreateMap<DbPrefixMaster, PrefixModel>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            
        }
    }
}
