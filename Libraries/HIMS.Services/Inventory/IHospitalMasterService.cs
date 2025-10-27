using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;

namespace HIMS.Services.Inventory
{
    public partial interface IHospitalMasterService
    {
        Task<IPagedList<HospitalMasterListDto>> GetListAsyncH(GridRequestModel objGrid);

    }
}
