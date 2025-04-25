using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
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
        //Task InsertAsyncSP(ServiceMaster objService, int UserId, string Username);
        Task UpdateAsync(ServiceMaster objService, int UserId, string Username);
        Task CancelAsync(ServiceMaster objService, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<BillingServiceDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PackageServiceInfoListDto>> GetListAsync1(GridRequestModel objGrid);
        Task<List<BillingServiceListDto>> GetServiceListwithGroupWise(int TariffId, int ClassId, string IsPathRad, string ServiceName);
        Task<List<ServiceMaster>> GetAllRadiologyTest();
        Task UpdateDifferTraiff(ServiceDetail ObjServiceDetail, int UserId, string Username);



    }
}
