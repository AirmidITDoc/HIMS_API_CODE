using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.Canteen
{
    public interface ICanteenService
    {
        Task UpdateDietPlanScheduleStatus(List<TDietPlanScheduleDetail> objList,string Flag,int CurrentUserId,string CurrentUserName);
        Task UpdateDietPlanDateStatus(DateTime PlanDate, string Flag, int CurrentUserId, string CurrentUserName);
    }
}
