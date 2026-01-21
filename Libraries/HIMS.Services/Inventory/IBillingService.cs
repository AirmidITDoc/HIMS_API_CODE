using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IBillingService
    {
        Task InsertAsync(ServiceMaster objService, int UserId, string Username);
        //Task InsertAsyncSP(ServiceMaster objService, int UserId, string Username);
        Task UpdateAsync(ServiceMaster objService, int UserId, string Username, string[]? references);
        Task CancelAsync(ServiceMaster objService, int CurrentUserId, string CurrentUserName);
        Task<IPagedList<BillingServiceDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<PackageServiceInfoListDto>> GetListAsync1(GridRequestModel objGrid);
        Task<List<BillingServiceListDto>> GetServiceListwithGroupWise(int TariffId, int ClassId, string IsPathRad, string ServiceName);


        List<BillingServiceDtoList> GetItemListForPrescriptionSearch(int TariffId, int ClassId, string SrvcName);

        Task<List<ServiceMaster>> GetAllRadiologyTest();
        void UpdateDifferTariff(ServiceDetail serviceDetail, long OldTariffId, long NewTariffId, int userId, string userName);
        Task<List<ServiceMasterDTO>> GetServiceListwithTraiff(int TariffId, string ServiceName);

        BillingServiceNewDto GetServiceListNew(int TariffId);
        Task SaveServicesNew(int TariffId, List<BillingServiceNew> Data);
        void Insert(List<MPackageDetail> ObjMPackageDetail, int UserId, string Username, long? PackageTotalDays, long? PackageIcudays, decimal? PackageMedicineAmount, decimal? PackageConsumableAmount);
        Task<IPagedList<PackageDetListDto>> GetListAsyncD(GridRequestModel objGrid);
        void InsertS(ServiceWiseCompanyCode ObjServiceWiseCompanyCode, int UserId, string Username);

    }
}
