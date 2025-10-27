using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;

namespace HIMS.Services.Pathlogy
{
    public partial interface IMPathParaRangeWithAgeMasterService
    {
        Task<IPagedList<MPathParaRangeWithAgeMasterListDto>> MPathParaRangeWithAgeMasterList(GridRequestModel objGrid);



    }
}
