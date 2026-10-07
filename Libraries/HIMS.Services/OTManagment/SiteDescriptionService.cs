using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OTManagment;
using HIMS.Data.Models;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public class SiteDescriptionService : ISiteDescriptionService
    {
        private readonly HIMSDbContext _context;

        public SiteDescriptionService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPagedList<SiteDescriptionListDto>> GetSiteDescriptionListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SiteDescriptionListDto>(model, "ps_rtrv_OTSurgeryTypeList");
        }
    }
}