using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;

namespace HIMS.Services.Inventory
{
    public partial interface ISubTPACompanyService
    {
        Task<IPagedList<SubTpaCompanyListDto>> GetListAsync(GridRequestModel objGrid);

    }
}
