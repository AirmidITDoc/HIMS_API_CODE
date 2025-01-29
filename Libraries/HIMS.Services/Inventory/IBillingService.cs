using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
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
        Task<IPagedList<BillingServiceDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<IPPaymentListDto>> GetListAsyncc(GridRequestModel objGrid);
        Task<IPagedList<IPBillListDto>> GetListAsyn(GridRequestModel objGrid);


        Task<List<ServiceMaster>> GetAllRadiologyTest();

    }
}

