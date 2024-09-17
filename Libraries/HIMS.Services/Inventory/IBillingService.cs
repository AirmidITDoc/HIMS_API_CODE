using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface IBillingService
    {
         Task InsertAsync(ServiceMaster objService, int UserId, string Username);
        Task InsertAsyncSP(ServiceMaster objService, int UserId, string Username);
        Task UpdateAsync(ServiceMaster objService, int UserId, string Username);
        Task CancelAsync(ServiceMaster objService, int CurrentUserId, string CurrentUserName);
        Task<List<ServiceMaster>> GetAllRadiologyTest();

    }
}

