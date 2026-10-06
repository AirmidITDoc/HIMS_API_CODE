using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OTManagment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HIMS.Services.OTManagment
{
    public interface ISubspecialityService
    {
        Task<IPagedList<SubspecialityListDto>> GetSubspecialityListAsync(GridRequestModel model);
    }
}