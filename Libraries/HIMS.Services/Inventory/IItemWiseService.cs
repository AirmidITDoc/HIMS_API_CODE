using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;

namespace HIMS.Services.Inventory
{
    public partial interface IItemWiseService
    {
        Task<IPagedList<ItemWiseListDto>> ItemWiseList(GridRequestModel objGrid);
    }
}
