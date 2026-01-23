using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface ICompanyMasterService
    {
        Task<IPagedList<CompanyMasterListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<ServiceTariffWiseListDto>> SGetListAsync(GridRequestModel objGrid);
        Task<IPagedList<ServiceCompanyTariffWiseListDto>> CGetListAsync(GridRequestModel objGrid);
        void Insertsp(List<ServiceWiseCompanyCode> ObjServiceWiseCompanyCode, int UserId, string UserName, long? userId);
        void Inserts(List<MCompanyWiseServiceDiscount> objCompanyWiseServiceDiscount, int UserId, string UserName, long? userId);
        Task UpdateAsync(ServiceDetail objServiceDetail, int UserId, string Username);

        Task InsertAsync(CompanyMaster objCompanyMaster, int UserId, string Username);

        Task UpdateAsync(CompanyMaster objCompanyMaster, int UserId, string Username, string[]? references);


    }
}
