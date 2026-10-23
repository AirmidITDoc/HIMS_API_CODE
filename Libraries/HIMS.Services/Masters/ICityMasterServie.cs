using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;

namespace HIMS.Services.Masters
{
    public interface ICityMasterServie
    {
        Task<IPagedList<DistrictLGDCodeDto>> DistrictLGDCodeListAsync(GridRequestModel model);
    }
}
