using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;

namespace HIMS.Services.Inventory
{
    public partial interface IWardMasterService
    {
        Task<IPagedList<WardMasterListDto>> GetListAsyncH(GridRequestModel objGrid);

    }
}
