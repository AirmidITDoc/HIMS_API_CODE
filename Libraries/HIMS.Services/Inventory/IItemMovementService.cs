using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;

namespace HIMS.Services.Inventory
{
    public partial interface IItemMovementService
    {
        Task<IPagedList<ItemMovementListDto>> ItemMovementList(GridRequestModel objGrid);


    }
}
