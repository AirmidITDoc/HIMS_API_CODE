using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ICompanyMasterService
    {
        Task<IPagedList<CompanyMasterListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<ServiceTariffWiseListDto>> SGetListAsync(GridRequestModel objGrid);
        Task<IPagedList<ServiceCompanyTariffWiseListDto>> CGetListAsync(GridRequestModel objGrid);
        Task InsertAsyncsp(List<ServiceWiseCompanyCode> ObjServiceWiseCompanyCode, int UserId, string UserName, long? userId);
        Task InsertAsyncs(List<MCompanyWiseServiceDiscount> objCompanyWiseServiceDiscount, int UserId, string UserName, long? userId);
        Task UpdateAsync(ServiceDetail objServiceDetail, int UserId, string Username);


    }
}
