using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public partial  interface IMenuMasterService
    {
        Task InsertAsyncSP(MenuMaster objMenuMaster, int UserId, string Username);
        Task InsertAsync(MenuMaster objMenuMaster, int UserId, string Username);
        Task UpdateAsyncSP(MenuMaster objMenuMaster, int UserId, string Username);
        Task<IPagedList<MenuMasterListDto>> MenuMasterList(GridRequestModel objGrid);

    }
}
