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
        Task UpdateDifferTariff(ServiceDetail serviceDetail, long OldTariffId, long NewTariffId, int userId, string userName);
        Task<List<ServiceMasterDTO>> GetServiceListwithTraiff(int TariffId, string ServiceName);
       
        Task<BillingServiceNewDto> GetServiceListNew(int TariffId);
        Task SaveServicesNew(int TariffId, List<BillingServiceNew> Data);
        Task InsertAsync(List<MPackageDetail> ObjMPackageDetail, int UserId, string Username, long? PackageTotalDays, long? PackageIcudays, decimal? PackageMedicineAmount, decimal? PackageConsumableAmount);
        Task<IPagedList<PackageDetListDto>> GetListAsyncD(GridRequestModel objGrid);
        Task  InsertAsyncS(ServiceWiseCompanyCode ObjServiceWiseCompanyCode, int UserId, string Username);

    }
}
