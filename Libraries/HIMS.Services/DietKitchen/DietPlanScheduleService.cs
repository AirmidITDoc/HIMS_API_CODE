using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using System.Threading.Tasks;

namespace HIMS.Services.DietKitchen
{
    public class DietPlanScheduleService : IDietPlanScheduleService
    {
        private readonly HIMSDbContext _context;

        public DietPlanScheduleService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPagedList<DietPlanScheduleListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietPlanScheduleListDto>(model, "ps_Rtrv_DietPlanSchedule");
        }
    }
}