using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.OTManagment;
using HIMS.Data.Models;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public class SubspecialityService : ISubspecialityService
    {
        private readonly HIMSDbContext _context;

        public SubspecialityService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPagedList<SubspecialityListDto>> GetSubspecialityListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SubspecialityListDto>(model, "ps_rtrv_OTSubspecialityList");
        }
    }
}