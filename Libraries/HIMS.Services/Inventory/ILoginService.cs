using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ILoginService
    {

        Task InsertAsync(LoginManager objLogin, int UserId, string Username);
        Task UpdateAsync(LoginManager objLogin, int UserId, string Username);
        Task CancelAsync(LoginManager objLogin, int UserId, string Username);
        Task updatepassAsync(LoginManager objLogin, int UserId, string Username);
        Task<IPagedList<LoginManagerListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<LoginGetMobileDto>> GetListAsyncg(GridRequestModel objGrid);

        Task<IPagedList<LoginConfigUserWiseListDto>> GetListAsyncL(GridRequestModel objGrid);
        Task<IPagedList<LoginStoreUserWiseListDto>> GetListAsyncLC(GridRequestModel objGrid);
        Task<IPagedList<LoginAccessConfigListDto>> GetListAsyncLA(GridRequestModel objGrid);
        Task<IPagedList<LoginUnitUserWiseListDto>> GetListAsyncLU(GridRequestModel objGrid);




    }
}
