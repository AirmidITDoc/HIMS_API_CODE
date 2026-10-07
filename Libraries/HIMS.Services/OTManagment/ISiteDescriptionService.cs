using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OTManagment;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public interface ISiteDescriptionService
    {
        Task<IPagedList<SiteDescriptionListDto>> GetSiteDescriptionListAsync(GridRequestModel model);
    }
}