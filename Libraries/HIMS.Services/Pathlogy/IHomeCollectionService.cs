using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public partial interface IHomeCollectionService
    {
        Task InsertAsync(THomeCollectionRegistrationInfo ObjTHomeCollectionRegistrationInfo, int UserId, string Username);
        Task UpdateAsync(THomeCollectionRegistrationInfo ObjTHomeCollectionRegistrationInfo, int UserId, string Username, string[]? ignoreColumns = null);
        Task<IPagedList<homeCollectionDetListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<HomeCollectionRegistrationInfoListDto>> HomeCollectionListAsync(GridRequestModel objGrid);
        Task Cancel(THomeCollectionRegistrationInfo objTHomeCollectionRegistrationInfo, int UserId, string Username);





    }
}
