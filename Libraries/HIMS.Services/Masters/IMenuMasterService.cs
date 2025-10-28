using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IMenuMasterService
    {
        void InsertSP(MenuMaster objMenuMaster, int UserId, string Username);
        void UpdateSP(MenuMaster objMenuMaster, int UserId, string Username);
        Task<IPagedList<MenuMasterListDto>> MenuMasterList(GridRequestModel objGrid);

    }
}
