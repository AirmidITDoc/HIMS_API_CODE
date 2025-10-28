using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Master;

namespace HIMS.Services.Inventory
{
    public partial interface IStoreMasterService
    {
        Task<IPagedList<StoreMasterListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
