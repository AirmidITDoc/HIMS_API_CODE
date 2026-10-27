using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HIMS.Services.DietKitchen
{
    public interface IDietPlanScheduleService
    {
        Task<IPagedList<DietPlanScheduleListDto>> GetListAsync(GridRequestModel model);
        Task<IPagedList<DietPlanDetailListDto>> DietplandetailsListAsync(GridRequestModel model);
        Task InsertAsync(TDietPlanSchedule objDietPlanSchedule, int UserId, string Username);
        Task UpdateAsync(TDietPlanSchedule objDietPlanSchedule, int UserId, string Username, string[]? ignoreColumns = null);

         
        Task Cancel(TDietPlanSchedule objDietPlanSchedule, int UserId, string Username);
        Task CancelD(TDietPlanScheduleDetail objDietPlanScheduleDetail, int UserId, string Username);
    }
}