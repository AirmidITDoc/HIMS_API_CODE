using HIMS.Core.Domain.Grid;
using System.Threading.Tasks;

namespace HIMS.Services.DietKitchen
{
    public interface IDietPlanScheduleService
    {
        Task<IPagedList<DietPlanScheduleListDto>> GetListAsync(GridRequestModel model);
    }
}